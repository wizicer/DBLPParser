namespace ExtractDBLPForm
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml;

    public partial class FrmDBLPExtract : Form
    {
        public FrmDBLPExtract()
        {
            InitializeComponent();
            this.txtDBLPfile.Text = @"C:\Data\dblp\dblp.xml";
        }

        private void FrmDBLPExtract_Load(object sender, EventArgs e)
        {
        }

        private void btnStart_Click(object sender, EventArgs eventarg)
        {
            //var keywords = "blockchain,merkle,bitcoin,ethereum,hyperledger,monero,eosio,algorand,zcash,filecoin,immutable";
            var keywords = "sgx,trusted execution,privacy,federat,enclave,trustzone,amd sev";
            //var yearstart = 2018;
            var words = keywords
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => _.Trim())
                .ToArray();
            var wordStats = words.ToDictionary(_ => _, _ => 0);

            var rs = GetRecords(this.txtDBLPfile.Text);
            //var fw = FilterByWords(rs, words, yearstart, wordStats)
            var yearstart = 2011;
            var yearend = 2022;
            var dbaclass = new[] {
                "journals/tods",
                "journals/tois",
                "journals/tkde",
                "journals/vldb",
                "conf/sigmod",
                "conf/kdd",
                "conf/icde",
                "conf/sigir",
                "conf/vldb",
            };
            var fw = FilterByKeyPrefix(rs, dbaclass, (yearstart, yearend))
                .Select(_ => new ExportPaper(_))
                .ToArray();
            var json = JsonConvert.SerializeObject(
                new { records = fw, stats = wordStats, filename = Path.GetFileName(this.txtDBLPfile.Text) },
                Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText(@"..\..\papers.js", "var papers = " + json);
        }

        private IEnumerable<DblpRecord> FilterByKeyPrefix(IEnumerable<DblpRecord> records, string[] keyPrefixes, (int start, int end) year)
        {
            var i = 0;
            var p = 0;
            foreach (var record in records)
            {
                i++;
                var isMatch = false;

                if (keyPrefixes.Any(_ => record.key.StartsWith(_)))
                    isMatch = true;

                // filter year
                if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || (y < year.start || y > year.end))))
                {
                    isMatch = false;
                }

                if (isMatch)
                {
                    p++;
                    yield return record;
                }

                this.UpdateProgress(i, p);
            }

            this.UpdateProgress(i, p, true);
        }
        private IEnumerable<DblpRecord> FilterByWords(IEnumerable<DblpRecord> records, string[] words, int yearstart, Dictionary<string, int> wordStats)
        {
            var i = 0;
            var p = 0;
            foreach (var record in records)
            {
                i++;
                var isMatch = false;
                // filter words
                foreach (var word in words)
                {
                    if (record.title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        isMatch = true;

                        // filter year
                        if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || y < yearstart)))
                        {
                            isMatch = false;
                        }

                        if (isMatch) wordStats[word]++;
                    }
                }

                if (isMatch)
                {
                    p++;
                    yield return record;
                }

                this.UpdateProgress(i, p);
            }

            this.UpdateProgress(i, p, true);
        }

        private void UpdateProgress(int totalProcessed, int found, bool isFinished = false)
        {
            if (!isFinished && totalProcessed % 10000 != 0) return;

            Application.DoEvents();
            var p = found;
            var i = totalProcessed;
            var progTick = 100_0000;

            this.Invoke((Action)(() =>
            {
                if (isFinished)
                {
                    this.lblStatus.Text = $"Finished [{p}/{i}]";
                    this.barProgress.Maximum = 100;
                    this.barProgress.Value = 100;
                }
                else
                {
                    this.lblStatus.Text = $"Processing {p}/{i} items";
                    var m = (i / progTick) + 1;
                    m = Math.Max(m, 9);
                    this.barProgress.Maximum = m * progTick;
                    this.barProgress.Value = i;
                }
            }));
        }

        private IEnumerable<DblpRecord> GetRecords(string dblpXmlFilePath)
        {
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                ValidationType = ValidationType.DTD,
                XmlResolver = new XmlUrlResolver(),
            };
            var reader = XmlReader.Create(Path.GetFullPath(dblpXmlFilePath), settings);

            var warnFields = new HashSet<string>();

            while (!reader.EOF)
            {
                reader.MoveToContent();
                if (reader.Depth != 1) { reader.Read(); continue; }
                if (reader.NodeType != XmlNodeType.Element) { reader.Read(); continue; }

                var ee = new List<string>();
                var author_names = new List<string>();
                var fields = new Dictionary<string, object>();
                var type = reader.Name;
                fields.Add("type", type);
                fields.Add("key", reader.GetAttribute("key"));
                fields.Add("mdate", reader.GetAttribute("mdate"));
                reader.Read();
                while (reader.Depth == 2)
                {
                    if (reader.NodeType == XmlNodeType.Whitespace) { reader.MoveToContent(); continue; }
                    if (reader.NodeType != XmlNodeType.Element) { continue; }

                    var entity = reader.Name;
                    switch (entity)
                    {
                        case "author":
                        case "editor":
                            var tmp = reader.ReadInnerXml();
                            author_names.Add(tmp);
                            break;

                        case "ee":
                            var tmpee = reader.ReadInnerXml();
                            if (tmpee.IndexOf("https://doi.org") > -1) AddField(fields, "doi", tmpee);
                            else ee.Add(tmpee);
                            break;

                        default:
                            var field = reader.ReadInnerXml();
                            AddField(fields, entity, field);
                            break;
                    };
                }

                fields.Add("authors", author_names.ToArray());
                fields.Add("ee", ee.ToArray());

                reader.Read();

                var ent = ProduceEntity(type, fields);
                if (ent == null) continue;
                yield return ent;
            }

            void AddField(Dictionary<string, object> fields, string name, string value)
            {
                if (fields.ContainsKey(name))
                {
                    if (!warnFields.Contains(name))
                    {
                        warnFields.Add(name);
                        Debug.WriteLine($"Find multiple fields [{name}]");
                    }
                }
                else
                {
                    fields.Add(name, value);
                }
            }
        }

        private DblpRecord ProduceEntity(string type, Dictionary<string, object> fields)
        {
            DblpRecord ent = null;
            switch (type)
            {
                case "article":
                    ent = new Article();
                    break;

                case "inproceedings":
                    ent = new Inproceeding();

                    break;

                case "phdthesis":
                    ent = new PhdThesis();
                    break;

                case "proceedings":
                    ent = new Proceeding();
                    break;

                case "www":
                    if (fields.ContainsKey("title") && fields["title"] is string title && title == "Home Page")
                    {
                        ent = new Www();
                    }
                    break;

                case "book":
                    ent = new Book();
                    break;

                case "incollection":
                    ent = new InCollection();
                    break;

                case "mastersthesis":
                    ent = new MasterThesis();
                    break;

                default:
                    break;
            }

            if (ent == null) return null;
            foreach (var kvp in fields)
            {
                var propertyInfo = ent.GetType().GetProperty(kvp.Key);
                if (propertyInfo == null) continue;
                propertyInfo.SetValue(ent, kvp.Value, null);
            }

            return ent;
        }
    }

    public static class Extensions
    {
        public static string ReadInnerXmlAndRegulate(this XmlReader reader)
            => reader.ReadInnerXml().Replace('~', '_').Replace("\"", "\"\"");

        public static Task WriteItemsLineAsync(this StreamWriter sb, string separator, params object[] args)
        {
            return sb.WriteLineAsync(string.Join(separator, args));
        }

        public static void WriteItemsLine(this StreamWriter sb, string separator, params object[] args)
        {
            sb.WriteLine(string.Join(separator, args));
        }
    }
}
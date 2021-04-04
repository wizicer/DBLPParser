namespace ExtractDBLPForm
{
    using System;
    using System.Collections.Generic;
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
            this.txtDBLPfile.Text = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01.xml";
        }

        private void FrmDBLPExtract_Load(object sender, EventArgs e)
        {
        }

        private async void btnStart_Click(object sender, EventArgs eventarg)
        {
            var d = GetRecords(this.txtDBLPfile.Text);
            var dd = d.Take(10).ToArray();
            var k = 1;
        }

        private IEnumerable<DblpRecord> GetRecords(string dblpXmlFilePath)
        {
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                ValidationType = ValidationType.DTD
            };
            var reader = XmlReader.Create(Path.GetFullPath(dblpXmlFilePath), settings);

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
                            var tmp = reader.ReadElementContentAsString();
                            author_names.Add(tmp);
                            break;

                        case "ee":
                            var tmpee = reader.ReadElementContentAsString();
                            if (tmpee.IndexOf("https://doi.org") > -1) fields.Add("doi", tmpee);
                            else ee.Add(tmpee);
                            break;

                        default:
                            var field = reader.ReadElementContentAsString();
                            fields.Add(entity, field);
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
                    if (fields["title"] is string title && title == "Home Page")
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
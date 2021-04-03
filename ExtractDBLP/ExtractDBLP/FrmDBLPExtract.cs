namespace ExtractDBLPForm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using System.Xml;
    using System.IO;

    public partial class FrmDBLPExtract : Form
    {
        public FrmDBLPExtract()
        {
            InitializeComponent();
            this.txtDBLPfile.Text = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01.xml";
            this.txtOutput.Text = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01\";
        }

        private void FrmDBLPExtract_Load(object sender, EventArgs e)
        {
        }

        private async void btnStart_Click(object sender, EventArgs eventarg)
        {
            var txtDBLPfile = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01.xml";
            var txtOutput = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01\";

            var d = GetRecords();
            var dd = d.Take(10).ToArray();
            var k = 1;

            IEnumerable<DblpRecord> GetRecords()
            {

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.ValidationType = ValidationType.DTD;
                XmlReader reader = XmlReader.Create(Path.GetFullPath(txtDBLPfile), settings);

                while (!reader.EOF)
                {
                    reader.MoveToContent();
                    if (reader.Depth != 1)
                    {
                        reader.Read();
                        continue;
                    }
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        reader.Read();
                        continue;
                    }

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
                        if (reader.NodeType == XmlNodeType.Whitespace)
                        {
                            reader.MoveToContent();
                        }
                        else if (reader.NodeType == XmlNodeType.Element)
                        {

                            var entity = reader.Name;
                            switch (entity)
                            {
                                case "author":
                                case "editor":
                                    string tmp = reader.ReadInnerXmlAndRegulate();
                                    author_names.Add(tmp);
                                    break;
                                case "ee":
                                    string tmpee = reader.ReadInnerXmlAndRegulate();
                                    if (tmpee.IndexOf("https://doi.org") > -1) fields.Add("doi", tmpee);
                                    else ee.Add(tmpee);
                                    break;
                                default:
                                    var field = reader.ReadElementContentAsString();
                                    fields.Add(entity, field);
                                    break;
                            };
                        }
                    }
                    fields.Add("authors", author_names.ToArray());
                    fields.Add("ee", ee.ToArray());

                    reader.Read();
                    var ent = ProduceEntity(type, fields);
                    if (ent == null) continue;
                    yield return ent;

                }

                DblpRecord ProduceEntity(string type, Dictionary<string, object> fields)
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

        }
    }
    public class DblpRecord
    {
        public string type { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string mdate { get; set; }
        public string title { get; set; }


        public string note { get; set; }
        public string crossref { get; set; }
        public string url { get; set; }
        public string[] authors { get; set; }
        public int count => authors.Length;
    };

    public class Www : DblpRecord
    {
    };

    public class Paper : DblpRecord
    {
        public string pages { get; set; }
        public string year { get; set; }
        public string[] ee { get; set; }
        public string doi { get; set; }

        public string volume { get; set; }
        public string journal { get; set; }

        public string booktitle { get; set; }

        public string school { get; set; }
        public string series { get; set; }
    };

    public class Article : Paper { }

    public class Inproceeding : Paper { }
    public class PhdThesis : Paper { }
    public class Proceeding : Paper { }
    public class Book : Paper { }
    public class InCollection : Paper { }
    public class MasterThesis : Paper { }

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

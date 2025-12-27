namespace DblpCli.Parsers;

using DblpCli.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

public class DblpParser
{
    public static IEnumerable<DblpRecord> GetRecords(string dblpXmlFilePath)
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

    private static DblpRecord ProduceEntity(string type, Dictionary<string, object> fields)
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

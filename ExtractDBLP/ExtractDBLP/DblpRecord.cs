namespace ExtractDBLPForm
{
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

    public class ExportPaper
    {
        public ExportPaper(DblpRecord record)
        {
            this.type = record.type;
            this.key = record.key;
            this.title = record.title;
            this.authors = record.authors;
            if (record is Paper p)
            {
                this.year = p.year;
                this.url = p.url;
                this.doi = p.doi;
                this.publisher
                    = p.journal != null ? p.journal
                    : p.booktitle != null ? p.booktitle
                    : p.school != null ? p.school
                    : null;

            }
        }
        public string type { get; set; }
        public string key { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string url { get; set; }
        public string doi { get; set; }
        public string publisher { get; set; }
        public string[] authors { get; set; }
    }
}
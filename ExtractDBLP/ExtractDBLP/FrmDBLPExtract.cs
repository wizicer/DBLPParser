using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Linq;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ExtractDBLPForm
{
    public partial class FrmDBLPExtract : Form
    {
        private const int pageSize = 200000000;
        object lock_w = new object();
        List<AuthorDBLP> listAs = new List<AuthorDBLP>();
        List<InproceedingsDBLP> listIs = new List<InproceedingsDBLP>();
        List<ConferenceDBLP> listCs = new List<ConferenceDBLP>();

        public FrmDBLPExtract()
        {
            InitializeComponent();
            this.txtDBLPfile.Text = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01.xml";
            this.txtOutput.Text = @"C:\Users\icer\Downloads\dblp\dblp-2021-03-01\";
        }

        private async void btnStart_Click(object sender, EventArgs eventarg)
        {
            this.Cursor = Cursors.WaitCursor;


            int globalAuthorsCounter = 0;
            int globalInproceedingsCounter = 0;
            int globalArticlesCounter = 0;
            int globalProceedingsCounter = 0;
            int globalBooksCounter = 0;
            int globalInCollectionsCounter = 0;
            int globalPhdthesisCounter = 0;
            int globalMasterthesisCounter = 0;

            int globalTitleCounter = 0;
            StringBuilder sbTitle = new StringBuilder();
            sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));

            var sbAuthor = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-www.csv"));
            sbAuthor.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "NOTE", "CROSSREF", "URL", "AUTHORS", "COUNT", "author_keys", "HASHCODE");
            var sbArticles = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-articles.csv"));
            sbArticles.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "PAGES", "YEAR", "VOLUME", "JOURNAL", "EE", "DOI", "URL", "CROSSREF", "AUTHORS", "COUNT");
            var sbInproceedings = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-inproceedings.csv"));
            sbInproceedings.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "PAGES", "YEAR", "BOOKTITLE", "EE", "DOI", "URL", "CROSSREF", "AUTHORS", "COUNT");
            var sbPhdThesis = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-phdthesis.csv"));
            sbPhdThesis.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "PAGES", "YEAR", "SCHOOL", "NOTE", "URL", "CROSSREF", "AUTHORS", "COUNT", "author_keys", "HASHCODE");
            var sbProceedings = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-proceedings.csv"));
            sbProceedings.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "VOLUME", "YEAR", "BOOKTITLE", "SERIES", "URL", "EDITORS", "COUNT", "author_keys", "HASHCODE");
            var sbBooks = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-books.csv"));
            sbBooks.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "VOLUME", "YEAR", "BOOKTITLE", "SERIES", "URL", "EDITORS", "COUNT", "author_keys", "HASHCODE");
            var sbInCollections = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-incollections.csv"));
            sbInCollections.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "VOLUME", "YEAR", "BOOKTITLE", "SERIES", "URL", "EDITORS", "COUNT", "author_keys", "HASHCODE");
            var sbMasterThesis = new StreamWriter(Path.GetFullPath(txtDBLPfile.Text + "-masterthesis.csv"));
            sbMasterThesis.WriteItemsLine("~", "ID", "KEY", "MDATE", "TITLE", "PAGES", "YEAR", "SCHOOL", "NOTE", "URL", "CROSSREF", "AUTHORS", "COUNT", "author_keys", "HASHCODE");

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            XmlReader reader = XmlReader.Create(Path.GetFullPath(txtDBLPfile.Text), settings);

            string key = reader.GetAttribute("key");
            string mdate = reader.GetAttribute("mdate");
            string title = "";
            string pages = "";
            string year = "";
            string volume = "";
            string journal = "";
            string ee = "";
            string doi = "";
            string url = "";
            string crossref = "";
            string booktitle = "";
            string author_names = "";
            string author_codes = "";
            string publtype = "";
            int a_count = 0;


            while (!reader.EOF)
            {
                reader.MoveToContent();
                if (reader.Depth != 1)
                {
                    reader.Read();
                    continue;
                }
                if (reader.NodeType == XmlNodeType.Element)
                {
                    title = "";
                    pages = "";
                    year = "";
                    volume = "";
                    journal = "";
                    ee = "";
                    doi = "";
                    url = "";
                    crossref = "";
                    booktitle = "";
                    author_names = "";
                    author_codes = "";
                    publtype = "";
                    a_count = 0;
                    switch (reader.Name)
                    {
                        case "article":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {

                                    switch (reader.Name)
                                    {
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "pages": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "volume": volume = reader.ReadInnerXmlAndRegulate(); break;
                                        case "journal": journal = reader.ReadInnerXmlAndRegulate(); break;
                                        case "ee":
                                            string tmpee = reader.ReadInnerXmlAndRegulate();
                                            if (tmpee.IndexOf("https://doi.org") > -1) doi = tmpee;
                                            else ee += (string.IsNullOrEmpty(ee) ? "" : "|") + tmpee;
                                            break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        case "crossref": crossref = reader.ReadInnerXmlAndRegulate(); break;
                                        case "publtype": publtype = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            //globalTitleCounter++, title,title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //  {
                            //      File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //      sbTitle.Clear();
                            //      sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //  }
                            await sbArticles.WriteItemsLineAsync("~",
                                globalArticlesCounter, key, mdate, title, pages,
                                year, volume, journal, ee, doi,
                                url, crossref, author_names, a_count);
                            globalArticlesCounter++;
                            break;
                        case "inproceedings":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "pages": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "booktitle": booktitle = reader.ReadInnerXmlAndRegulate(); break;
                                        case "ee":
                                            string tmpee = reader.ReadInnerXmlAndRegulate();
                                            if (tmpee.IndexOf("https://doi.org") > -1) doi = tmpee;
                                            else ee += (string.IsNullOrEmpty(ee) ? "" : "|") + tmpee;
                                            break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        case "crossref": crossref = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            //globalTitleCounter++, title,title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //{
                            //  File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //  sbTitle.Clear();
                            //  sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //}
                            await sbInproceedings.WriteItemsLineAsync("~",
                                globalInproceedingsCounter, key, mdate, title, pages,
                                year, booktitle, ee, doi, url,
                                crossref, author_names, a_count);
                            globalInproceedingsCounter++;
                            break;
                        case "phdthesis":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "pages": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "school": booktitle = reader.ReadInnerXmlAndRegulate(); break;
                                        case "note": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        case "crossref": crossref = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            // globalTitleCounter++, title, title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //{
                            //  File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //  sbTitle.Clear();
                            //  sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //}
                            sbPhdThesis.WriteLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}",
                                globalPhdthesisCounter, key, mdate, title, pages, year, booktitle, ee, url, crossref, author_names, a_count,
                                author_codes, title.GetHashCode().ToString()));
                            globalPhdthesisCounter++;
                            break;
                        case "proceedings":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "editor":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "volume": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "booktitle": booktitle = reader.ReadInnerXmlAndRegulate(); break;
                                        case "series": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            // globalTitleCounter++, title, title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //{
                            //  File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //  sbTitle.Clear();
                            //  sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //}
                            sbProceedings.WriteLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}",
                                globalProceedingsCounter, key, mdate, title, pages, year, booktitle, ee, url, author_names, a_count,
                                author_codes, title.GetHashCode().ToString()));
                            globalProceedingsCounter++;
                            break;
                        case "www":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate().Replace('~', '_'); break;
                                        case "note": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "crossref": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            if (title == "Home Page")
                            {
                                await sbAuthor.WriteItemsLineAsync("~",
                                    globalAuthorsCounter, key, mdate, title, pages, ee, url, author_names, a_count, author_codes);
                                globalAuthorsCounter++;

                            }
                            break;
                        case "book":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "editor":
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "volume": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "booktitle": booktitle = reader.ReadInnerXmlAndRegulate(); break;
                                        case "series": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            // globalTitleCounter++, title, title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //{
                            //  File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //  sbTitle.Clear();
                            //  sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //}
                            sbBooks.WriteLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}",
                                globalBooksCounter++, key, mdate, title, pages, year, booktitle, ee, url, author_names,
                                a_count, author_codes, title.GetHashCode().ToString()));

                            break;
                        case "incollection":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "editor":
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "volume": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "booktitle": booktitle = reader.ReadInnerXmlAndRegulate(); break;
                                        case "series": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "ee": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            //  globalTitleCounter++, title, title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //{
                            //  File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //  sbTitle.Clear();
                            //  sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //}
                            sbInCollections.WriteLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}",
                                globalInCollectionsCounter++, key, mdate, title, pages, year, booktitle, ee, url,
                                author_names, a_count, author_codes, title.GetHashCode().ToString()));
                            break;
                        case "mastersthesis":
                            key = reader.GetAttribute("key");
                            mdate = reader.GetAttribute("mdate");
                            reader.Read();
                            while (reader.Depth == 2)
                            {
                                if (reader.NodeType == XmlNodeType.Whitespace)
                                {
                                    reader.MoveToContent();
                                }
                                else if (reader.NodeType == XmlNodeType.Element)
                                {
                                    switch (reader.Name)
                                    {
                                        case "author":
                                            string tmp = reader.ReadInnerXmlAndRegulate();
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXmlAndRegulate(); break;
                                        case "pages": pages = reader.ReadInnerXmlAndRegulate(); break;
                                        case "year": year = reader.ReadInnerXmlAndRegulate(); break;
                                        case "school": booktitle = reader.ReadInnerXmlAndRegulate(); break;
                                        case "note": ee = reader.ReadInnerXmlAndRegulate(); break;
                                        case "url": url = reader.ReadInnerXmlAndRegulate(); break;
                                        case "crossref": crossref = reader.ReadInnerXmlAndRegulate(); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            //sbTitle.AppendLine(string.Format("{0}~{1}~{2}",
                            // globalTitleCounter++, title, title.GetHashCode().ToString()));
                            //if (globalTitleCounter % 500000 == 0)
                            //{
                            //  File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + ".csv"), sbTitle.ToString());
                            //  sbTitle.Clear();
                            //  sbTitle.AppendLine(string.Format("ID~TITLE~HASHCODE"));
                            //}
                            sbMasterThesis.WriteLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}",
                                globalMasterthesisCounter++, key, mdate, title, pages, year, booktitle, ee, url, crossref, author_names,
                                a_count, author_codes, title.GetHashCode().ToString()));

                            break;
                        default:
                            reader.Read();
                            break;
                    }
                }
                else
                {
                    reader.Read();
                }

            }
            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-title-" + (globalTitleCounter / 500000) + "-end.csv"), sbTitle.ToString());

            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-articles-" + (globalArticlesCounter / pageSize) + "-end.csv"), sbArticles.ToString());
            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-inproceedings-" + (globalInproceedingsCounter / pageSize) + "-end.csv"), sbInproceedings.ToString());
            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-incollections-" + (globalInCollectionsCounter / pageSize) + "-end.csv"), sbInCollections.ToString());

            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-phdthesis-" + (globalPhdthesisCounter / pageSize) + "-end.csv"), sbPhdThesis.ToString());
            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-masterthesis-" + (globalPhdthesisCounter / pageSize) + "-end.csv"), sbMasterThesis.ToString());
            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-books-" + (globalBooksCounter / pageSize) + "-end.csv"), sbBooks.ToString());

            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-proceedings-" + (globalProceedingsCounter / pageSize) + "-end.csv"), sbProceedings.ToString());
            //File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-www-" + (globalAuthorsCounter / pageSize) + "-end.csv"), sbAuthor.ToString());

            sbAuthor.Close();
            sbArticles.Close();
            sbInproceedings.Close();
            sbPhdThesis.Close();
            sbProceedings.Close();
            sbBooks.Close();
            sbInCollections.Close();
            sbMasterThesis.Close();

            MessageBox.Show("Done");
            this.Cursor = Cursors.Default;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
        }

        private void FrmDBLPExtract_Load(object sender, EventArgs e)
        {
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
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

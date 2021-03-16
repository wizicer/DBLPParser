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

        private void btnStart_Click(object sender, EventArgs eventarg)
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

            StringBuilder sbAuthor = new StringBuilder();
            sbAuthor.AppendLine(string.Format("ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbArticles = new StringBuilder();
            sbArticles.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~VOLUME~JOURNAL~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbInproceedings = new StringBuilder();
            sbInproceedings.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~BOOKTITLE~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbPhdThesis = new StringBuilder();
            sbPhdThesis.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~SCHOOL~NOTE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbProceedings = new StringBuilder();
            sbProceedings.AppendLine(string.Format("ID~KEY~MDATE~TITLE~VOLUME~YEAR~BOOKTITLE~SERIES~URL~EDITORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbBooks = new StringBuilder();
            sbBooks.AppendLine(string.Format("ID~KEY~MDATE~TITLE~VOLUME~YEAR~BOOKTITLE~SERIES~URL~EDITORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbInCollections = new StringBuilder();
            sbInCollections.AppendLine(string.Format("ID~KEY~MDATE~TITLE~VOLUME~YEAR~BOOKTITLE~SERIES~URL~EDITORS~COUNT~author_keys~HASHCODE"));
            StringBuilder sbMasterThesis = new StringBuilder();
            sbMasterThesis.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~SCHOOL~NOTE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));

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
            string url = "";
            string crossref = "";
            string booktitle = "";
            string author_names = "";
            string author_codes = "";
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
                    url = "";
                    crossref = "";
                    booktitle = "";
                    author_names = "";
                    author_codes = "";
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "pages": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "volume": volume = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "journal": journal = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "ee": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "crossref": crossref = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbArticles.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}`{14}",
                                      globalArticlesCounter, key, mdate, title, pages, year, volume, journal, ee, url, crossref, author_names,
                                      a_count, author_codes, title.GetHashCode().ToString()));
                            globalArticlesCounter++;
                            if (globalArticlesCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-articles-" + (globalArticlesCounter / pageSize) + ".csv"), sbArticles.ToString());
                                sbArticles.Clear();
                                sbArticles.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~VOLUME~JOURNAL~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
                            }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "pages": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "booktitle": booktitle = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "ee": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "crossref": crossref = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbInproceedings.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}",
                                globalInproceedingsCounter, key, mdate, title, pages, year, booktitle, ee, url, crossref, author_names,
                                a_count, author_codes, title.GetHashCode().ToString()));
                            globalInproceedingsCounter++;
                            if (globalInproceedingsCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-inproceedings-" + (globalInproceedingsCounter / pageSize) + ".csv"), sbInproceedings.ToString());
                                sbInproceedings.Clear();
                                sbInproceedings.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~BOOKTITLE~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
                            }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "pages": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "school": booktitle = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "note": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "crossref": crossref = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbPhdThesis.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}",
                                globalPhdthesisCounter, key, mdate, title, pages, year, booktitle, ee, url, crossref, author_names, a_count,
                                author_codes, title.GetHashCode().ToString()));
                            globalPhdthesisCounter++;
                            if (globalPhdthesisCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-phdthesis-" + (globalPhdthesisCounter / pageSize) + ".csv"), sbPhdThesis.ToString());
                                sbPhdThesis.Clear();
                                sbPhdThesis.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~SCHOOL~NOTE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
                            }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "volume": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "booktitle": booktitle = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "series": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbProceedings.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}",
                                globalProceedingsCounter, key, mdate, title, pages, year, booktitle, ee, url, author_names, a_count,
                                author_codes, title.GetHashCode().ToString()));
                            globalProceedingsCounter++;
                            if (globalProceedingsCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-proceedings-" + (globalProceedingsCounter / pageSize) + ".csv"), sbProceedings.ToString());
                                sbProceedings.Clear();
                                sbProceedings.AppendLine(string.Format("ID~KEY~MDATE~TITLE~VOLUME~YEAR~BOOKTITLE~SERIES~URL~EDITORS~COUNT~author_keys~HASHCODE"));
                            }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_').Replace('~', '_'); break;
                                        case "note": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "crossref": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
                                        default: reader.ReadElementContentAsString(); break;
                                    };
                                }
                            }
                            reader.Read();
                            if (title == "Home Page")
                            {
                                sbAuthor.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}",
                                    globalAuthorsCounter, key, mdate, title, pages, ee, url, author_names, a_count, author_codes));
                                globalAuthorsCounter++;

                                if (globalAuthorsCounter % pageSize == 0)
                                {
                                    File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-www-" + (globalAuthorsCounter / pageSize) + ".csv"), sbAuthor.ToString());
                                    sbAuthor.Clear();
                                    sbAuthor.AppendLine(string.Format("ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys"));
                                }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "volume": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "booktitle": booktitle = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "series": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbBooks.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}",
                                globalBooksCounter++, key, mdate, title, pages, year, booktitle, ee, url, author_names,
                                a_count, author_codes, title.GetHashCode().ToString()));

                            if (globalBooksCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-books-" + (globalBooksCounter / pageSize) + ".csv"), sbBooks.ToString());
                                sbBooks.Clear();
                                sbBooks.AppendLine(string.Format("ID~KEY~MDATE~TITLE~VOLUME~YEAR~BOOKTITLE~SERIES~URL~EDITORS~COUNT~author_keys~HASHCODE"));
                            }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "volume": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "booktitle": booktitle = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "series": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "ee": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbInCollections.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}",
                                globalInCollectionsCounter++, key, mdate, title, pages, year, booktitle, ee, url,
                                author_names, a_count, author_codes, title.GetHashCode().ToString()));
                            if (globalInCollectionsCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-incollections-" + (globalInCollectionsCounter / pageSize) + ".csv"), sbInCollections.ToString());
                                sbInCollections.Clear();
                                sbInCollections.AppendLine(string.Format("ID~KEY~MDATE~TITLE~VOLUME~YEAR~BOOKTITLE~SERIES~URL~EDITORS~COUNT~author_keys~HASHCODE"));
                            }
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
                                            string tmp = reader.ReadInnerXml().Replace('~', '_');
                                            author_names += "|" + tmp;
                                            author_codes += "|" + tmp.GetHashCode();
                                            a_count++; break;
                                        case "title": title = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "pages": pages = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "year": year = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "school": booktitle = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "note": ee = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "url": url = reader.ReadInnerXml().Replace('~', '_'); break;
                                        case "crossref": crossref = reader.ReadInnerXml().Replace('~', '_'); break;
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
                            sbMasterThesis.AppendLine(string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}",
                                globalMasterthesisCounter++, key, mdate, title, pages, year, booktitle, ee, url, crossref, author_names,
                                a_count, author_codes, title.GetHashCode().ToString()));

                            if (globalMasterthesisCounter % pageSize == 0)
                            {
                                File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-masterthesis-" + (globalMasterthesisCounter / pageSize) + ".csv"), sbMasterThesis.ToString());
                                sbMasterThesis.Clear();
                                sbMasterThesis.AppendLine(string.Format("ID~KEY~MDATE~TITLE~PAGES~YEAR~SCHOOL~NOTE~URL~CROSSREF~AUTHORS~COUNT~author_keys~HASHCODE"));
                            }
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

            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-articles-" + (globalArticlesCounter / pageSize) + "-end.csv"), sbArticles.ToString());
            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-inproceedings-" + (globalInproceedingsCounter / pageSize) + "-end.csv"), sbInproceedings.ToString());
            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-incollections-" + (globalInCollectionsCounter / pageSize) + "-end.csv"), sbInCollections.ToString());

            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-phdthesis-" + (globalPhdthesisCounter / pageSize) + "-end.csv"), sbPhdThesis.ToString());
            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-masterthesis-" + (globalPhdthesisCounter / pageSize) + "-end.csv"), sbMasterThesis.ToString());
            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-books-" + (globalBooksCounter / pageSize) + "-end.csv"), sbBooks.ToString());

            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-proceedings-" + (globalProceedingsCounter / pageSize) + "-end.csv"), sbProceedings.ToString());
            File.WriteAllText(Path.GetFullPath(txtDBLPfile.Text + "-www-" + (globalAuthorsCounter / pageSize) + "-end.csv"), sbAuthor.ToString());

            MessageBox.Show("Done");
            this.Cursor = Cursors.Default;
        }

        private void BuildConference(Dictionary<ConferenceDBLP, int> ConferenceToId)
        {
            dblpData dataDBLP = new dblpData();
            dblp_conference conference;
            foreach (ConferenceDBLP a in ConferenceToId.Keys)
            {
                conference = new dblp_conference()
                {
                    name = a.Name,
                    id = ConferenceToId[a],
                    inproceedings = string.Join("|", a.Inproceedings),
                    count = a.Inproceedings.Count,
                };
                dataDBLP.dblp_conference.Add(conference);
            }
            dataDBLP.SaveChanges();
        }

        private void BuildAuthorToId(Dictionary<AuthorDBLP, int> authorToId)
        {
            dblpData dataDBLP = new dblpData();
            dblp_author author;
            foreach (AuthorDBLP a in authorToId.Keys)
            {
                author = new dblp_author()
                {
                    name = a.Name,
                    id = authorToId[a],
                    inproceedings = string.Join("|", a.Inproceedings),
                    count = a.Inproceedings.Count
                };
                dataDBLP.dblp_author.Add(author);
            }
            dataDBLP.SaveChanges();
        }

        private void BuildInproceedings(Dictionary<InproceedingsDBLP, int> inProceedingToId)
        {
            dblpData dataDBLP = new dblpData();
            dblp_inproceedings inproceeding;
            foreach (InproceedingsDBLP a in inProceedingToId.Keys)
            {
                inproceeding = new dblp_inproceedings()
                {
                    key = a.Key,
                    title = a.Title,
                    id = inProceedingToId[a],
                    authors = string.Join("|", a.Authors),
                    count = a.Authors.Count,
                    conference = a.Conference,
                    conference_id = a.ConferenceID
                };
                dataDBLP.dblp_inproceedings.Add(inproceeding);
            }
            dataDBLP.SaveChanges();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            //string connStr = "server=localhost;user=root;database=dblp;port=3306;password=;";
            //MySqlConnection conn = new MySqlConnection(connStr);
            //try
            //{
            //    Console.WriteLine("Connecting to MySQL...");
            //    conn.Open();
            //    // Perform database operations
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //conn.Close();
            //Console.WriteLine("Done.");

        }

        private void FrmDBLPExtract_Load(object sender, EventArgs e)
        {
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            LoadDataFromDB();
            MessageBox.Show("Done Load");
            this.Cursor = Cursors.Default;
        }

        private void LoadDataFromDB()
        {
            using (dblpData dataDBLP = new dblpData())
            {
                List<dblp_author> listAuthors = dataDBLP.dblp_author.ToList();
                List<dblp_conference> listConferences = dataDBLP.dblp_conference.ToList();
                List<dblp_inproceedings> listInproceedings = dataDBLP.dblp_inproceedings.ToList();

                listAs.Clear();
                listIs.Clear();
                listCs.Clear();

                AuthorDBLP aDBLP;
                foreach (dblp_author a in listAuthors)
                {
                    aDBLP = new AuthorDBLP()
                    {
                        OldValue = 0,
                        CurrentValue = Convert.ToInt32(txtStartValue.Text),
                        Id = a.id,
                        Name = a.name,
                        CountInproceedings = a.count == null ? 0 : (int)a.count,
                        Inproceedings = new List<int>()
                    };
                    foreach (string id in a.inproceedings.TrimStart('|').Split('|'))
                    {
                        aDBLP.Inproceedings.Add(Convert.ToInt32(id));
                    }
                    aDBLP.InproceedingsID = aDBLP.Inproceedings.Aggregate("", (cur, next) => cur + "|" + next);
                    listAs.Add(aDBLP);
                }
                InproceedingsDBLP iDBLP;
                foreach (dblp_inproceedings i in listInproceedings)
                {
                    iDBLP = new InproceedingsDBLP()
                    {
                        Id = i.id,
                        Title = i.title,
                        Key = i.key,
                        Year = i.year,
                        Pages = i.pages,
                        Crossref = i.crossref,
                        Conference = i.conference,
                        AuthorsID = "",
                        ConferenceID = i.conference_id == null ? 0 : (int)i.conference_id,
                        CountAuthors = i.count == null ? 0 : (int)i.count,
                        Authors = new List<int>()
                    };
                    foreach (string id in i.authors.TrimStart('|').Split('|'))
                    {
                        iDBLP.Authors.Add(Convert.ToInt32(id));
                    }
                    iDBLP.AuthorsID = iDBLP.Authors.Aggregate("", (cur, next) => cur + "|" + next);
                    listIs.Add(iDBLP);
                }
                ConferenceDBLP cDBLP;
                foreach (dblp_conference c in listConferences)
                {
                    cDBLP = new ConferenceDBLP()
                    {
                        Id = c.id,
                        Name = c.name,
                        CountInproceedings = c.count == null ? 0 : (int)c.count,
                        Inproceedings = new List<int>()
                    };
                    foreach (string id in c.inproceedings.TrimStart('|').Split('|'))
                    {
                        cDBLP.Inproceedings.Add(Convert.ToInt32(id));
                    }
                    cDBLP.InproceedingsID = cDBLP.Inproceedings.Aggregate("", (cur, next) => cur + "|" + next);
                    listCs.Add(cDBLP);
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            double sumAs1, sumIs1, sumCs, sumIs2, sumAs2, min, ratio, max;
            max = 1000;
            while (max > Convert.ToDouble(txtMinstop.Text))
            {
                sumAs1 = listAs.Sum(x => x.CurrentValue);
                listIs.ForEach(x => x.SetValueFromAuthors(listAs));
                sumIs1 = listIs.Sum(x => x.CurrentValue);
                listCs.ForEach(x => x.SetValueFromInproceedings(listIs));
                sumCs = listCs.Sum(x => x.CurrentValue);
                listIs.ForEach(x => x.SetValueFromConferences(listCs));
                sumIs2 = listIs.Sum(x => x.CurrentValue);
                listAs.ForEach(x => x.SetValueFromInproceedings(listIs));
                sumAs2 = listAs.Sum(x => x.CurrentValue);
                ratio = sumAs2 / sumAs1;
                min = listAs.Min(x => Math.Abs(x.CurrentValue - x.OldValue));
                max = listAs.Max(x => Math.Abs(x.CurrentValue - x.OldValue));
            }
            this.dgvAuthors.DataSource = listAs;
            this.dgvInproceedings.DataSource = listIs;
            this.dgvConferences.DataSource = listCs;
            MessageBox.Show("Done");
            this.Cursor = Cursors.Default;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            Thread tr = new Thread(new ThreadStart(UpdateAuthor));
            tr.Start();
        }
        private void UpdateAuthor()
        {
            var sw = Stopwatch.StartNew();
            List<table_articles> listArticles = new List<table_articles>();
            List<table_inproceedings> listInproceedings = new List<table_inproceedings>();

            List<table_phdthesis> listPhdthesis = new List<table_phdthesis>();
            List<table_proceedings> listProceedings = new List<table_proceedings>();
            List<table_www> listWWWs = new List<table_www>();
            int start = Convert.ToInt32(txtAuthorIDStart.Text);
            int end = Convert.ToInt32(txtAuthorIDEnd.Text);

            using (dblpData dataDBLP = new dblpData())
            {
                if (chkUpdateAll.Checked)
                {
                    listWWWs = dataDBLP.table_www.Where(a => a.ID >= start && a.ID <= end && a.COUNT == 1).ToList();
                }
                else
                {
                    listWWWs = dataDBLP.table_www.Where(a => a.ID >= start && a.ID <= end && a.COUNT == 1 && (a.inproceedings_count == 0 || a.inproceedings_count == null)).ToList();
                }
                //listWWWs = dataDBLP.table_www.Where(a => a.ID == 49).ToList();

                listWWWs.AsParallel().ForAll(w => AssignAuthorPaperList(w, ref listAs, ref listIs, ref listCs));
                dataDBLP.SaveChanges();
            }
            sw.Stop();


            Console.WriteLine("Total: {0}:{1}:{2}", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
            // running on worker thread
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsg.Text += string.Format("Total: {0}:{1}:{2}", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);

                    dgvAuthors.Invoke((MethodInvoker)delegate
                    {
                        dgvAuthors.DataSource = listAs;
                        dgvAuthors.Refresh();

                    });
                    dgvInproceedings.Invoke((MethodInvoker)delegate
                    {
                        dgvInproceedings.DataSource = listIs;
                        dgvInproceedings.Refresh();
                        dgvConferences.Refresh();
                        dgvConferences.DataSource = listCs;
                    });
                    dgvConferences.Invoke((MethodInvoker)delegate
                    {
                        dgvConferences.DataSource = listCs;
                        dgvConferences.Refresh();
                    });
                });

            }
            MessageBox.Show("Done");
        }
        private void AssignAuthorPaperList(table_www w, ref List<AuthorDBLP> listAuthors, ref List<InproceedingsDBLP> listInDBLP,
            ref List<ConferenceDBLP> listConferences)
        {
            List<table_inproceedings> listIns = new List<table_inproceedings>();
            AuthorDBLP authorDBLP = null;
            string author = w.author_keys;
            var sw = Stopwatch.StartNew();
            var id = w.ID;
            using (dblpData dataDBLP = new dblpData())
            {
                try
                {
                    if (dataDBLP.table_inproceedings.Count() > 0)
                    {
                        List<table_inproceedings> listI = dataDBLP.table_inproceedings.Where(
                                a => a.author_keys.Contains(author + "|") || a.author_keys.EndsWith(author)).ToList();
                        if (listI.Count > 0)
                        {
                            listIns = listIns.Union(listI).ToList();
                            var inproceedings =
                                from l in listIns select new InproceedingsDBLP(l.ID, l.KEY, l.TITLE, l.BOOKTITLE, Convert.ToString(l.YEAR), l.author_keys, (int)l.COUNT, 0);
                            var conferences =
                               from l in listIns.GroupBy(i => i.BOOKTITLE).Select(
                                   g => new
                                   {
                                       key = g.Key,
                                       count = g.Count(),
                                       inproceedings = g.Aggregate(string.Empty, (x, i) => x + "|" + i.ID)
                                   })
                               select new ConferenceDBLP(l.key, (int)l.count, l.inproceedings);
                            lock (lock_w)
                            {
                                listInDBLP = listInDBLP.Union(inproceedings).ToList();
                                listConferences.AddRange(conferences);
                            }
                            table_www www = dataDBLP.table_www.Where(a => a.ID == id).FirstOrDefault();
                            www.inproceedings_count = w.inproceedings_count = listIns.Count;
                            authorDBLP = new AuthorDBLP()
                            {
                                Id = id,
                                CountInproceedings = listIns.Count,
                                InproceedingsID = www.inproceedings_key = w.inproceedings_key = listIns.Aggregate("", (x, y) => x + "|" + y.ID),
                                CurrentValue = 0,
                                OldValue = 0
                            };
                            dataDBLP.SaveChanges();
                            listAuthors.Add(authorDBLP);
                        }

                        sw.Stop();
                        // running on worker thread
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtMsg.Text += string.Format("wwwID -{0} -{1}: {2:F2}s\r\n", id, author, sw.Elapsed.TotalSeconds);

                                if (txtMsg.Text.Length > 10000)
                                    txtMsg.Text = "";
                            });
                        }
                        else
                        {
                            Thread.CurrentThread.Abort();
                        }
                        Console.WriteLine("wwwID -{0} -{1}: {2:F2}s\r\n", id, author, sw.Elapsed.TotalSeconds);
                    }
                }
                catch (Exception ex)
                {
                    // running on worker thread
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            txtMsg.Text += string.Format("wwwID - {0} - {1} - msg:{2}\r\n", id, author, ex.StackTrace);// runs on UI thread
                        });
                    }
                    else
                    {
                        Thread.CurrentThread.Abort();
                    }
                    Console.WriteLine("wwwID -{0} -{1}: {2:F2}s\r\n", id, author, sw.Elapsed.TotalSeconds);
                }
            }
            this.Invoke((MethodInvoker)delegate
            {
                dgvAuthors.Invoke((MethodInvoker)delegate
                {
                    dgvAuthors.DataSource = listAs;
                    dgvAuthors.Refresh();

                });
                dgvInproceedings.Invoke((MethodInvoker)delegate
                {
                    dgvInproceedings.DataSource = listIs;
                    dgvInproceedings.Refresh();
                });
                dgvConferences.Invoke((MethodInvoker)delegate
                {
                    dgvConferences.DataSource = listCs;
                    dgvConferences.Refresh();
                });
            });
        }

    }
}

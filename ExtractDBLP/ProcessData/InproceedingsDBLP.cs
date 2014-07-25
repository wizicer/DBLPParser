using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessData
{
    public class InproceedingsDBLP : IEquatable<InproceedingsDBLP>
    {
        private int  m_id;
        private int m_lineIndex;
        public int LineIndex
        {
            get { return m_lineIndex; }
            set { m_lineIndex = value; }
        }
        private int m_fileIndex;
        public int FileIndex
        {
            get { return m_fileIndex; }
            set { m_fileIndex = value; }
        }
        private string m_key;
        private string m_title;
        private string m_conference;
        private string m_year;
        private string m_pages;        
        private string m_crossref;
        private string m_listAuthorsId;
        public string AuthorsID
        {
            get { return m_listAuthorsId; }
            set { m_listAuthorsId = value; }
        }
      
        private int m_conference_id;

        private double m_old_value;
        private double m_cur_value;
        public double OldValue
        {
            get { return m_old_value; }
            set { m_old_value = value; }
        }
        public double CurrentValue
        {
            get { return m_cur_value; }
            set { m_cur_value = value; }
        }

        public double SetValueFromAuthors(Dictionary<int,AuthorDBLP> allAuthors)
        {
            OldValue = CurrentValue;
            List<string> authorids = AuthorsID.Split('|').ToList();
            CurrentValue = 0;
            foreach (string next in authorids)
            {
                int i;
                if (int.TryParse(next, out i))
                {
                    CurrentValue+= allAuthors[i].CurrentValue / allAuthors[i].CountInproceedings;
                }                
            }
            return CurrentValue;
        }
        public double SetValueFromConferences(Dictionary<int,ConferenceDBLP> allConferences)
        {
            OldValue = CurrentValue;
            return CurrentValue = allConferences[ConferenceID].CurrentValue / allConferences[ConferenceID].CountInproceedings;
        }
        public double SetValueFromConferences(Dictionary<string, ConferenceDBLP> allConferences)
        {
            OldValue = CurrentValue;
            string[] keys = Key.Split('/');
            string conf_key = keys.Length > 1 ? keys[1] : keys[0];
            return CurrentValue = allConferences[conf_key].CurrentValue / allConferences[conf_key].CountInproceedings;
        }
        private int m_countAuthors;

        public int CountAuthors
        {
            get { return m_countAuthors; }
            set { m_countAuthors = value; }

        }
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }
        private string m_MDATE;
        public string MDATE
        {
            get { return m_MDATE; }
            set { m_MDATE = value; }
        }
        public string Title
        {
            get { return m_title; }
            set { m_title= value; }
        }
        public string Conference
        {
            get { return m_conference; }
            set { m_conference = value; }
        }
        public string Year
        {
            get { return m_year; }
            set { m_year = value; }
        }
        public string Pages
        {
            get { return m_pages; }
            set { m_pages= value; }
        }
        public string Crossref
        {
            get { return m_crossref; }
            set { m_crossref = value; }
        }
       
        public int ConferenceID
        {
            get { return m_conference_id; }
            set { m_conference_id = value; }
        }
        private string m_EE;
        public string EE
        {
            get { return m_EE; }
            set { m_EE = value; }
        }
        private string m_URL;
        public string URL
        {
            get { return m_URL; }
            set { m_URL = value; }
        }
        private string m_CROSSREF;
        public string CROSSREF
        {
            get { return m_CROSSREF; }
            set { m_CROSSREF = value; }
        }
        private string m_AUTHORNAMEs;
        public string AUTHORNAMEs
        {
            get { return m_AUTHORNAMEs; }
            set { m_AUTHORNAMEs = value; }
        }
        public InproceedingsDBLP()
        {
        }

        public InproceedingsDBLP(string key, string title, string conference,string pages, string year)
        {
            m_key = key;
            m_title = title;
            m_conference = conference;
            m_pages = pages;
            m_year = year;
        }
        public InproceedingsDBLP(int id, string key, string title, string conference, string year, string author_keys,int countAuthors, int curvalue)
        {
            m_id = id;
            m_key = key;
            m_title = title;
            m_conference = conference;
            m_countAuthors = countAuthors;
            m_year = year;
            m_cur_value = curvalue;           
            m_listAuthorsId = author_keys;
            
        }

        public bool Equals(InproceedingsDBLP other)
        {
            return other.Key.Equals(this.m_key);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as InproceedingsDBLP);
        }

        public override int GetHashCode()
        {
            return m_key.GetHashCode();
        }

        internal static InproceedingsDBLP CreateFromLine(string line, int lineIndex, int fileIndex, int year)
        {
            //ID~KEY~MDATE~TITLE~PAGES~YEAR~BOOKTITLE~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~conferenceid~LineIndex~FileIndex,
            InproceedingsDBLP a = null;
            int id;
            string[] datas = line.Split('~');
            if (int.TryParse(datas[0], out id))
            {
                if (Convert.ToInt32(datas[5]) > year)
                {
                    return null;
                }
                a = new InproceedingsDBLP
                {
                    Id = id,                   
                    Key = datas[1],
                    MDATE = datas[2],
                    Title = datas[3],
                    Pages =datas[4],
                    Year = datas[5],
                    Conference = datas[6],
                    EE = datas[7],
                    URL =datas[8],
                    CROSSREF =datas[9],
                    AUTHORNAMEs = datas[10],
                    CountAuthors = Convert.ToInt32(datas[11]),
                    AuthorsID = datas[12],
                    ConferenceID = datas.Length > 13 ? Convert.ToInt32(datas[13]) : 0,
                    LineIndex = lineIndex,
                    FileIndex = fileIndex,
                    OldValue = datas.Length > 16 ? Convert.ToInt32(datas[16]) : 0,
                    CurrentValue = datas.Length > 17 ? Convert.ToInt32(datas[17]) : 0,
                };
            }
            return a;            
        }
        public override string ToString()
        {
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}~{14}~{15}~{16}~{17}",
              Id, Key, MDATE, Title, Pages, Year , 
              Conference, EE, URL, CROSSREF, AUTHORNAMEs, 
              CountAuthors, AuthorsID, ConferenceID, LineIndex, FileIndex, (int) OldValue, (int) CurrentValue);                            
      
        }

        internal void SetValueFromAuthors(HashSet<int> authorCals, Dictionary<int, int> authorCountInproceedingsByYear)
        {
            throw new NotImplementedException();
        }
    }
    public class compactInproceedingsDBLP : IEquatable<InproceedingsDBLP>
    { 
        public int Key{get;set;}
        public HashSet<int> Authors{get;set;}
        public int Count {get;set;}
        public string Title {get;set;}
        public string Conference {get;set;}
        public int Year {get;set;}
        public double OldValue { get; set; }
        public double CurrentValue { get; set; }
        public int iline { get; set; }
        public int ifile { get; set; }

        public compactInproceedingsDBLP(string key, string title, string conference, string year, string author_keys, int old, int cur, int line, int file)
        {
            Key = Convert.ToInt32(key);
            Title= title;
            Conference = conference;
            Year = Convert.ToInt32(year);
            OldValue = old;
            CurrentValue = cur;
            string[] ids = author_keys.Trim('|').Split('|');
            int i;
            Authors = new HashSet<int>();
            foreach (string id in ids)
            {
                if (int.TryParse(id, out i))
                    Authors.Add(i);
            }
            Count = Authors.Count;
            iline = line;
            ifile = file;
        }

        public bool Equals(InproceedingsDBLP other)
        {
            return other.Key.Equals(this.Key);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as InproceedingsDBLP);
        }

        public override int GetHashCode()
        {
            return Key;
        }

        internal static compactInproceedingsDBLP CreateFromLine(string line, int lineIndex, int fileIndex, int year)
        {
            //ID~KEY~MDATE~TITLE~PAGES~YEAR~BOOKTITLE~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~conferenceid~LineIndex~FileIndex,
            compactInproceedingsDBLP a = null;
            int id;
            string[] datas = line.Split('~');
            if (int.TryParse(datas[0], out id))
            {
                if (Convert.ToInt32(datas[5]) > year)
                {
                    return null;
                }
                string[] confkeys = datas[1].Split('/');
                int oldv= datas.Length > 16 ? Convert.ToInt32(datas[16]) : 0;
                int newv = datas.Length > 17 ? Convert.ToInt32(datas[16]) : 0;
                a = new compactInproceedingsDBLP(datas[0], datas[3], confkeys[1], datas[5], datas[12], oldv, newv, lineIndex, fileIndex);
            }
            return a;            
        }
        public override string ToString()
        {
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}",
              Key, Title, Year, Conference, (int) OldValue, (int) CurrentValue);     
        }

        public double SetValueFromAuthors(Dictionary<int, compactAuthorDBLP> allAuthors)
        {
            OldValue = CurrentValue;          
            CurrentValue = 0;
            CurrentValue = Authors.Sum(i => allAuthors[i].CurrentValue / allAuthors[i].Count);
            //foreach (int i in Authors)
            //{
            //    CurrentValue += allAuthors[i].CurrentValue / allAuthors[i].Count;               
            //}
            return CurrentValue;
        }
        public double SetValueFromConferences(Dictionary<string, ConferenceDBLP> allConferences)
        {
            OldValue = CurrentValue;
            return CurrentValue = allConferences[Conference].CurrentValue / allConferences[Conference].CountInproceedings;
        }       
    }
}

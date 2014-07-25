using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessData
{
    public class AuthorDBLP : IEquatable<AuthorDBLP>
    {
        private int m_id;
        private long m_lineIndex;
        public long LineIndex
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
        private string m_name;
        private string m_author_keys;
        private string m_listInproceedingsId;
        public string InproceedingsID
        {
            get { return m_listInproceedingsId; }
            set { m_listInproceedingsId = value; }
        }
        //private Dictionary<int, InproceedingsDBLP> m_listInproceedings = new  Dictionary<int, InproceedingsDBLP>(2000);
        private int m_countInproceedings;

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


        public double SetValueFromInproceedings(Dictionary<int, InproceedingsDBLP> allInproceedings)
        {
            OldValue = CurrentValue;           
            List<string> inids = InproceedingsID.Split('|').ToList();
            CurrentValue = 0;
            foreach (string next in inids)
            {
                int i;
                if (int.TryParse(next, out i))
                {
                    CurrentValue += allInproceedings[i].CurrentValue / allInproceedings[i].CountAuthors;
                }                
            }
            return CurrentValue;
        }
        public int CountInproceedings
        {
            get { return m_countInproceedings; }
            set { m_countInproceedings = value; }
        }
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Author_Keys
        {
            get { return m_author_keys; }
            set { m_author_keys = value; }
        }
        //public Dictionary<int, InproceedingsDBLP> Inproceedings
        //{
        //    get { return m_listInproceedings; }
        //    set { m_listInproceedings = value; }
        //}

        //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs
        private string m_key;      
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }
        
        private string m_COUNT;
        public string COUNT
        {
            get { return m_COUNT; }
            set { m_COUNT = value; }
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
        private string m_NOTE;
        public string NOTE
        {
            get { return m_NOTE; }
            set { m_NOTE = value; }
        }
        private string m_MDATE;
        public string MDATE
        {
            get { return m_MDATE; }
            set { m_MDATE = value; }
        }
        private string m_title;
        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }
        public AuthorDBLP()
        {
        }
        public static AuthorDBLP CreateFromLine(string line, long lineIndex, int fileIndex, int value)
        {
            AuthorDBLP a=null;
            int id;
            string[] datas = line.Split('~');
            if (int.TryParse(datas[0], out id))
            { //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex

                a = new AuthorDBLP
                {
                    m_id = id,
                    m_key = datas[1],
                    m_MDATE = datas[2],
                    m_title = datas[3],
                    m_NOTE = datas[4],
                    m_CROSSREF = datas[5],
                    m_URL = datas[6],
                    m_name = datas[7],
                    m_COUNT = datas[8],
                    m_author_keys = datas[9].TrimStart('|'),
                    m_countInproceedings = datas.Length > 10 ? Convert.ToInt32(datas[10]) : 0,
                    m_listInproceedingsId = datas.Length > 11 ? datas[11] : "",
                    m_lineIndex = lineIndex,
                    m_fileIndex = fileIndex,
                    m_old_value = (value > 0) ? value : (datas.Length > 14 ? Convert.ToInt32(datas[14]) : 0),
                    m_cur_value = (value > 0) ? value : (datas.Length > 15 ? Convert.ToInt32(datas[14]) : 0),
                };
            }            
            return a;
        }
        public AuthorDBLP(string name, string homePage)
        {
            m_name = name;
            m_author_keys = homePage;
        }
        
        public bool Equals(AuthorDBLP other)
        {
            return other.Name.Equals(this.m_name);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AuthorDBLP);
        }

        public override int GetHashCode()
        {
            return m_name.GetHashCode();
        }

        public override string ToString()
        {
            //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}~{14}~{15}",
                  Id,Key,MDATE,Title,NOTE,CROSSREF,URL,Name, COUNT, Author_Keys, CountInproceedings, InproceedingsID, LineIndex, FileIndex, (int)OldValue, (int)CurrentValue);                             
        }
        public void UpdateInproceedings(int key, InproceedingsDBLP inproceeding)
        {
            if (!(m_listInproceedingsId.Contains("|" + inproceeding.Id + "|") || m_listInproceedingsId.EndsWith("|" + inproceeding.Id)))
            {
                m_listInproceedingsId += "|" + inproceeding.Id;
                m_countInproceedings++;
            }
            //m_listInproceedings.Add(key, inproceeding);
        }
    }
    public class compactAuthorDBLP : IEquatable<AuthorDBLP>
    {
        public int Key{get;set;}
        public HashSet<int> Papers { get; set; }
        public int Count { get; set; }
        public double OldValue { get; set; }
        public double CurrentValue { get; set; }

        public compactAuthorDBLP(string key, string paperIds, double old, double cur)
        {
            Papers = new HashSet<int>();
            Key = Convert.ToInt32(key);
            string[] ids = paperIds.Split('|');
            int i;
            foreach (string id in ids)
            {
                if (int.TryParse(id,out i))
                    Papers.Add(i);
            }
            Count = Papers.Count;
            OldValue = old;
            CurrentValue = cur;
        }
        
        public bool Equals(AuthorDBLP other)
        {
            return other.Name.Equals(this.Key);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AuthorDBLP);
        }

        public override int GetHashCode()
        {
            return Key;
        }
        public static compactAuthorDBLP CreateFromLine(string line, long lineIndex, int fileIndex, int value)
        {
            compactAuthorDBLP a = null;
            int id;
            string[] datas = line.Split('~');
            if (int.TryParse(datas[9].TrimStart('|'), out id))
            { //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex
                if (value > 0)
                {
                    a = new compactAuthorDBLP(datas[9].TrimStart('|'), datas[11], Convert.ToInt32(datas[14]), Convert.ToInt32(datas[15]));
                }
                else 
                {
                    a = new compactAuthorDBLP(datas[9].TrimStart('|'), datas[11], value, value);
                }
            }
            return a;
        }
        public override string ToString()
        {
            string s = Papers.Aggregate("", (x,y) => x + "|"+ y.ToString()); 
            //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex
            return string.Format("{0}~{1}~{2}~{3}~{4}",
                  Key,s,Count, (int)OldValue, (int)CurrentValue);                             
        }
        public double SetValueFromInproceedings(Dictionary<int, compactInproceedingsDBLP> allInproceedings)
        {
            OldValue = CurrentValue;
            CurrentValue = 0;
            foreach (int i in Papers)
            {
                CurrentValue += allInproceedings[i].CurrentValue / allInproceedings[i].Count;
            }
            return CurrentValue;
        }
    }
}

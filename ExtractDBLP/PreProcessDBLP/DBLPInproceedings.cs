using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessDBLP
{
    public class DBLPInproceedings: IEquatable<DBLPInproceedings>
    { 
        public int Key{get;set;}
        public string StringKey { get; set; }
        public HashSet<int> CitationIds { get; set; }
        public int CitationCount { get; set; }
        public HashSet<int> Authors{get;set;}
        public int AuthorCount {get;set;}
        public string Title {get;set;}
        public int Conference {get;set;}
        public int Year {get;set;}
        public Decimal OldValue { get; set; }
        public Decimal CurrentValue { get; set; }
        public int iline { get; set; }
        public int ifile { get; set; }
        public int type { get; set; } //=0: inproceeding, =1 article
        public DBLPInproceedings()
        {
            CitationIds = null;
            Authors = null;
        }
        public DBLPInproceedings(int t,string key, string strkey, string title, int conference, string year, string author_keys, int old, int cur, int line, int file, string citationIds)
        {
            if (string.Empty == year) year = "0";
            type = t;
            //Key = Convert.ToInt32(key);
            Key = (t.ToString() + "_" + strkey).GetHashCode();
            StringKey = strkey;
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
            AuthorCount = Authors.Count;

            ids = citationIds.Trim('|').Split('|');
            CitationIds = new HashSet<int>();
            foreach (string id in ids)
            {
                if (int.TryParse(id, out i))
                    CitationIds.Add(i);
            }
            CitationCount = CitationIds.Count;

            iline = line;
            ifile = file;
        }

        public bool Equals(DBLPInproceedings other)
        {
            if (other == null) return false;
            return other.Key.Equals(this.Key);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DBLPInproceedings);
        }

        public override int GetHashCode()
        {
            return Key;
        }

        internal static DBLPInproceedings CreateInproceedingsFromLine(int t,string line, int lineIndex, int fileIndex, int year)
        {
            //ID~KEY~MDATE~TITLE~PAGES~YEAR~BOOKTITLE~EE~URL~CROSSREF~AUTHORS~COUNT~author_keys~conferenceid~LineIndex~FileIndex,
            DBLPInproceedings a = null;
           
            int id;
            string[] datas = line.Split('~');
            if (int.TryParse(datas[0], out id))
            {
                if ((datas[5] != string.Empty && Convert.ToInt32(datas[5]) > year) || (t == 0 && datas[12] == string.Empty) || (t == 1 && datas[13] == string.Empty))
                {
                    return null;
                }
                string[] confkeys = datas[1].Split('/');
                int oldv = datas.Length > 16 ? Convert.ToInt32(datas[16]) : 0;
                int newv = datas.Length > 17 ? Convert.ToInt32(datas[16]) : 0;
                a = new DBLPInproceedings(t, datas[0], datas[1], datas[3], confkeys[1].GetHashCode(), datas[5], t==0? datas[12]:datas[13], oldv, newv, lineIndex, fileIndex, "");
            }
            return a;
        }
        public override string ToString()
        {
            string authors = Authors.Aggregate("", (x, y) => x + "|" + y.ToString());
            string citations = CitationIds == null ? "" : CitationIds.Aggregate("", (x, y) => x + "|" + y.ToString());
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}",
              Key, Title, Year, Conference, authors,AuthorCount,citations,CitationCount, (int) OldValue, (int) CurrentValue);     
        }

        public Decimal SetValueFromAuthors(Dictionary<int, DblpAuthors> allAuthors)
        {
            OldValue = CurrentValue;          
            CurrentValue = 0;
            CurrentValue = Authors.Sum(i => allAuthors[i].CurrentValue / allAuthors[i].Count);          
            return CurrentValue;
        }
        public Decimal SetValueFromConferences(Dictionary<int, DBLPConferences> allConferences)
        {
            OldValue = CurrentValue;
            return CurrentValue = allConferences[Conference].CurrentValue / allConferences[Conference].CountAll;
        }
        public Decimal SetValue(Dictionary<int, DblpAuthors> allAuthors, Dictionary<int, DBLPConferences> allConferences, Decimal ratio1, Decimal ratio2, Decimal total, Decimal start)
        {
            OldValue = CurrentValue;
            CurrentValue = 0;
            Decimal authorSum=Authors.Sum(i => allAuthors[i].CurrentValue / allAuthors[i].Count);
            CurrentValue = ratio1 * authorSum + ratio2 * allConferences[Conference].CurrentValue / allConferences[Conference].CountAll + (1 - ratio1 - ratio2) * start;
            return CurrentValue;
        }
    }
}

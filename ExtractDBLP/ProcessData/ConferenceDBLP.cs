using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessData
{
    public class ConferenceDBLP : IEquatable<ConferenceDBLP>
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
        private string m_booktitle;
        private string m_crossref;
      
        private string m_listInproceedingsId;
        public string InproceedingsID
        {
            get { return m_listInproceedingsId; }
            set { m_listInproceedingsId = value; }
        }
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

        public double SetValueFromInproceedings(Dictionary<int,InproceedingsDBLP> allInproceedings)
        {
            OldValue = CurrentValue;
            List<string> inproceedingsId = InproceedingsID.Split('|').ToList();
            return CurrentValue = inproceedingsId.AsParallel().Sum(next => {
                int i;
                if (int.TryParse(next, out i))
                {
                    return allInproceedings[i].CurrentValue;
                }
                return 0;
            });
        }
        private int m_countInproceedings;

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
        private string m_key;
        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }
        public string Name
        {
            get { return m_booktitle; }
            set { m_booktitle = value; }
        }

        public string Crossref
        {
            get { return m_crossref; }
            set { m_crossref = value; }
        }
        
        public ConferenceDBLP()
        {
        }
        public ConferenceDBLP(string booktitle)
        {
            m_booktitle = booktitle;           
        }
        public ConferenceDBLP(string key, string booktitle, int countInproceedings, string InproceedingsId, int id)
        {
            m_key = key;
            m_booktitle = booktitle;
            m_countInproceedings = countInproceedings;
            m_listInproceedingsId = "|"+InproceedingsId;
            m_id = id;
        }

        public bool Equals(ConferenceDBLP other)
        {
            return other.Name.Equals(this.m_booktitle);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ConferenceDBLP);
        }

        public override int GetHashCode()
        {
            return m_booktitle.GetHashCode();
        }


        internal static ConferenceDBLP CreateFromLine(string line, int lineIndex, int fileIndex)
        {
            ConferenceDBLP a = null;
            int id;
            string[] datas = line.Split('~');
            if (int.TryParse(datas[0], out id))
            {
                a = new ConferenceDBLP
                {
                    Id = id,   
                    Key =datas[1],
                    Name = datas[2],
                    CountInproceedings =Convert.ToInt32(datas[3]),
                    InproceedingsID = datas[4],    
                    LineIndex = lineIndex,
                    FileIndex = fileIndex,
                    OldValue = datas.Length > 7 ? Convert.ToInt32(datas[7]) : 0,
                    CurrentValue = datas.Length > 8 ? Convert.ToInt32(datas[8]) : 0,
                };
            }
            return a;
        }
        public override string ToString()
        {
            //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}",
                  Id, Key, Name, CountInproceedings,InproceedingsID, LineIndex, FileIndex,(int)OldValue,(int)CurrentValue);
        }
        public double SetValueFromInproceedings(Dictionary<int, compactInproceedingsDBLP> allInproceedings)
        {
            OldValue = CurrentValue;
            List<string> inproceedingsId = InproceedingsID.Split('|').ToList();
            return CurrentValue = inproceedingsId.AsParallel().Sum(next =>
            {
                int i;
                if (int.TryParse(next, out i))
                {
                    return allInproceedings[i].CurrentValue;
                }
                return 0;
            });
        }
    }
}

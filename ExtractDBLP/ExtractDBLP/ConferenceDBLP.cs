using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractDBLPForm
{
    public class ConferenceDBLP : IEquatable<ConferenceDBLP>
    {
        private int m_id;
        private string m_booktitle;
        private string m_crossref;
        private List<int> m_listInproceedings = new List<int>();
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

        public double SetValueFromInproceedings(List<InproceedingsDBLP> allInproceedings)
        {
            OldValue = CurrentValue;
            return CurrentValue = Inproceedings.Sum(next => allInproceedings[next].CurrentValue);
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
        public List<int> Inproceedings
        {
            get { return m_listInproceedings; }
            set { m_listInproceedings = value; }
        }
        public ConferenceDBLP()
        {
        }
        public ConferenceDBLP(string booktitle)
        {
            m_booktitle = booktitle;           
        }
        public ConferenceDBLP(string booktitle, int countInproceedings, string InproceedingsId)
        {
            m_booktitle = booktitle;
            m_countInproceedings = countInproceedings;
            m_listInproceedingsId = InproceedingsId;
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
        
    }
}

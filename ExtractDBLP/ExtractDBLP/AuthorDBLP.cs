using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractDBLPForm
{
    public class AuthorDBLP : IEquatable<AuthorDBLP>
    {
        private int m_id;
        private string m_name;
        private string m_homePage;
        private string m_listInproceedingsId;
        public string InproceedingsID
        {
            get { return m_listInproceedingsId; }
            set { m_listInproceedingsId = value; }
        }
        private List<int> m_listInproceedings = new List<int>();
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


        public double SetValueFromInproceedings(List<InproceedingsDBLP> allInproceedings)
        {
            OldValue = CurrentValue;
            return CurrentValue = Inproceedings.Sum(next => allInproceedings[next].CurrentValue/allInproceedings[next].CountAuthors);
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

        public string HomePage
        {
            get { return m_homePage; }
            set { m_homePage = value; }
        }
        public List<int> Inproceedings
        {
            get { return m_listInproceedings; }
            set { m_listInproceedings = value; }
        }
        public AuthorDBLP()
        {
        }
        public AuthorDBLP(string name, string homePage)
        {
            m_name = name;
            m_homePage = homePage;
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
        

    }
}

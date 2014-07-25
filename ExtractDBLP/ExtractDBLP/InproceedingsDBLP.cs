using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractDBLPForm
{
    public class InproceedingsDBLP : IEquatable<InproceedingsDBLP>
    {
        private int  m_id;        
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
        private List<int> m_listAuthorDBLP=new List<int>();
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

        public double SetValueFromAuthors(List<AuthorDBLP> allAuthors)
        {
            OldValue = CurrentValue;
            return CurrentValue = Authors.Sum(next => allAuthors[next].CurrentValue / allAuthors[next].CountInproceedings);
        }
        public double SetValueFromConferences(List<ConferenceDBLP> allConferences)
        {
            OldValue = CurrentValue;
            return CurrentValue = allConferences[ConferenceID].CurrentValue / allConferences[ConferenceID].CountInproceedings;
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
        public List<int> Authors
        {
            get { return m_listAuthorDBLP; }
            set { m_listAuthorDBLP = value; }
        }
        public int ConferenceID
        {
            get { return m_conference_id; }
            set { m_conference_id = value; }
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
    }
}

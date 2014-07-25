using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessDBLP
{
    public class DblpAuthors : IEquatable<DblpAuthors>
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public HashSet<int> Inproceedings { get; set; }
        public HashSet<int> Alias { get; set; }
        public int Count { get; set; }
        public Decimal OldValue { get; set; }
        public Decimal CurrentValue { get; set; }
        public int iline { get; set; }
        public int ifile { get; set; }
        public DblpAuthors(string key, string name, string paperIds)
        {
            Inproceedings = new HashSet<int>();          
            Key = Convert.ToInt32(key);
            Name = name;
            string[] ids = paperIds.Split('|');
            int i;
            foreach (string id in ids)
            {
                if (int.TryParse(id, out i))
                    Inproceedings.Add(i);
            }
            Count = Inproceedings.Count;
        }
        public DblpAuthors(string key, string name, string paperIds, HashSet<int> alias, Decimal old, Decimal cur, int file, int line)
        {
            Inproceedings = new HashSet<int>();
            Alias = alias;
            Key = Convert.ToInt32(key);
            Name = name;
            string[] ids = paperIds.Split('|');
            int i;
            foreach (string id in ids)
            {
                if (int.TryParse(id, out i))
                    Inproceedings.Add(i);
            }
            Count = Inproceedings.Count;
            OldValue = old;
            CurrentValue = cur;
            iline = line;
            ifile = file;
        }

        public bool Equals(DblpAuthors other)
        {
            if (other == null) return false;
            return other.Name.Equals(this.Key);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DblpAuthors);
        }

        public override int GetHashCode()
        {
            return Key;
        }
        public static DblpAuthors CreateFromAuthorsLine(string line, long lineIndex, int fileIndex, int value, int ifile, int iline)
        {
            DblpAuthors a = null;
            HashSet<int> authoralias =null;
            int ids;
            string[] datas = line.Split('~');
            if ("Home Page" == datas[3])
            {
                if (int.TryParse(datas[8], out ids))
                { //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex
                    string[] aus = datas[9].TrimStart('|').Split('|');
                    int author_id;
                    if (int.TryParse(aus[0], out author_id))
                    {
                        if (ids > 1)
                        {
                            authoralias = new HashSet<int>();
                            for (int i = 1; i < ids; i++)
                            {
                                if (!authoralias.Contains(Convert.ToInt32(aus[i])))
                                    authoralias.Add(Convert.ToInt32(aus[i]));
                            }
                        }
                        if (value > 0)
                        {
                            a = new DblpAuthors(aus[0], datas[7], "", authoralias, Convert.ToInt32(datas[14]), Convert.ToInt32(datas[15]), ifile, iline);
                        }
                        else
                        {
                            a = new DblpAuthors(aus[0], datas[7], "", authoralias, value, value, ifile,iline);
                        }
                    }
                }
            }
            return a;
        }
        public override string ToString()
        {
            string papers = Inproceedings.Aggregate("", (x, y) => x + "|" + y.ToString());
            string alias = Alias == null ? "" :  Alias.Aggregate("", (x, y) => x + "|" + y.ToString());
            //ID~KEY~MDATE~TITLE~NOTE~CROSSREF~URL~AUTHORS~COUNT~author_keys~InproceedingsCount~InproceedingsIDs~lineIndex, fileindex
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}",
                  Key, Name, alias, papers, Count, (int)OldValue, (int)CurrentValue);
        }
        public Decimal SetValueFromInproceedings(Dictionary<int, DBLPInproceedings> allInproceedings)
        {
            OldValue = CurrentValue;
            CurrentValue = 0;
            foreach (int i in Inproceedings)
            {
                CurrentValue += allInproceedings[i].CurrentValue / allInproceedings[i].AuthorCount;
            }
            return CurrentValue;
        }
    }
}

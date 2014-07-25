using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessDBLP
{
    public class DBLPConferences: IEquatable<DBLPConferences>
    {    
        public int Key { get; set; }
        public string Name { get; set; }
        public HashSet<int> Years { get; set; }
        public Dictionary<int, HashSet<int>> InproceedingsByYear { get; set; }
        public int CountAll { get; set; }
        public Decimal OldValue { get; set; }
        public Decimal CurrentValue { get; set; }

        public DBLPConferences(int key, string name, int year, int inpro, int count, int old, int cur)
        {
            Key = key;
            Name = name;
            Years = new HashSet<int>();
            Years.Add(year);
            InproceedingsByYear = new Dictionary<int,HashSet<int>>();
            InproceedingsByYear.Add(year, new HashSet<int>());
            InproceedingsByYear[year].Add(inpro);
            CountAll = count;
            OldValue = old;
            CurrentValue = cur;
        }
        public Decimal SetValueFromInproceedings(Dictionary<int, DBLPInproceedings> allInproceedings)
        {
            OldValue = CurrentValue;
            CurrentValue = 0;
            foreach (int year in Years)
            {
                foreach (int inpro in InproceedingsByYear[year])
                {
                    CurrentValue += allInproceedings[inpro].CurrentValue;
                }
            }
            return CurrentValue;
            
        }

        public bool Equals(DBLPConferences other)
        {
            if (other == null) return false;
            return other.Name.Equals(this.Key);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DBLPConferences);
        }


        public override int GetHashCode()
        {
            return Key;
        }
        public override string ToString()
        {
            string years = Years.Aggregate("", (x, y) => x + "|" + y.ToString());
            string inpro = InproceedingsByYear == null ? "" : InproceedingsByYear.Aggregate("", (x, y) => x + "+" + y.Key + "|" + y.Value.Count);
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}",
              Key, Name, CountAll, inpro , (int)OldValue, (int)CurrentValue);     
        } 
    }
}

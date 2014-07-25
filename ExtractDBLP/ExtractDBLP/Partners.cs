using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractDBLPForm
{
    class Partners : IEquatable<Partners>
    {
        private string m_partner1;
        private string m_partner2;

        public string Partner1
        {
            get { return m_partner1; }
        }

        public string Partner2
        {
            get { return m_partner2; }
        }

        public Partners(string partner1, string partner2)
        {
            if (partner1.Equals(partner2, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ApplicationException("Two coworkers can not be identical.");
            }
            m_partner1 = partner1;
            m_partner2 = partner2;
        }

        public bool Equals(Partners other)
        {
            bool first = other.Partner1.Equals(this.Partner1, StringComparison.CurrentCultureIgnoreCase) ||
                other.Partner1.Equals(this.Partner2, StringComparison.CurrentCultureIgnoreCase);

            bool second = other.Partner2.Equals(this.Partner1, StringComparison.CurrentCultureIgnoreCase) ||
                other.Partner2.Equals(this.Partner2, StringComparison.CurrentCultureIgnoreCase);

            return first && second;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Partners);
        }

        public override int GetHashCode()
        {
            return m_partner1.GetHashCode() ^ m_partner2.GetHashCode();
        }

    }
}

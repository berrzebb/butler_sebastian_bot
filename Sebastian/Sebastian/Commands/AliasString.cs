using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sebastian
{
    public class AliasString : IEqualityComparer<AliasString>
    {
        private readonly IList<string> strings;
        public AliasString(string[] strings)
        {
            this.strings = strings.ToList();
        }
        public AliasString(string str)
        {
            this.strings = Enumerable.Empty<string>().ToList();
            strings.Add(str);
        }
        public bool Contains(string s)
        {
            return strings.Contains(s);
        }

        public bool Equals(AliasString x, AliasString y)
        {
            return x.strings.SequenceEqual(y.strings);
        }


        public int GetHashCode(AliasString obj)
        {
            return obj.strings.GetHashCode();
        }
        public override string ToString()
        {
            return string.Join("|", strings);
        }
    }
}

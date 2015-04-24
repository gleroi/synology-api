using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synology.Api
{
    public struct Parameter
    {
        public string Key { get; private set; }
        public IEnumerable<string> Values { get; private set; }

        public Parameter(string key, params string[] values)
            : this()
        {
            this.Key = key;
            this.Values = new List<string>(values);
        }

        internal string ToUrlParam()
        {
            return this.Key + "=" + String.Join(",", this.Values);
        }
    }
}

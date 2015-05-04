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
        public IEnumerable<object> Values { get; private set; }

        public Parameter(string key, params object[] values)
            : this()
        {
            this.Key = key;
            this.Values = new List<object>(values);
        }

        internal string ValuesAsString()
        {
            return String.Join(",", this.Values);
        }

        internal string ToUrlParam()
        {
            if (this.Values != null & this.Values.Any())
            {
                return this.Key + "=" + this.ValuesAsString();
            }
            return String.Empty;
        }

        internal static string Join(string separator, IEnumerable<Parameter> parameters)
        {
            var strs = parameters.Select(p => p.ToUrlParam())
                .Where(str => !String.IsNullOrEmpty(str));

            return String.Join(separator, strs);
        }
    }
}

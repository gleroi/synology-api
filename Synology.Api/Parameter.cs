using System.Collections.Generic;
using System.Linq;

namespace Synology.Api
{
    public struct Parameter
    {
        public string Key { get; private set; }
        public IEnumerable<object> Values { get; private set; }

        public Parameter(string key, params object[] values)
            : this()
        {
            Key = key;
            Values = new List<object>(values);
        }

        internal string ValuesAsString()
        {
            return string.Join(",", Values);
        }

        internal string ToUrlParam()
        {
            if (Values != null & Values.Any())
            {
                return Key + "=" + ValuesAsString();
            }
            return string.Empty;
        }

        internal static string Join(string separator, IEnumerable<Parameter> parameters)
        {
            var strs = parameters.Select(p => p.ToUrlParam())
                .Where(str => !string.IsNullOrEmpty(str));

            return string.Join(separator, strs);
        }
    }
}
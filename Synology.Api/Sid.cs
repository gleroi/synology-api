using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synology.Api
{
    public struct Sid
    {
        public string Value { get; private set; }

        public Sid(string value)
            : this()
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value", "value cannot be null or empty");
            }
            this.Value = value;
        }
    }
}

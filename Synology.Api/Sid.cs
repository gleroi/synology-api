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
            this.Value = value;
        }
    }
}

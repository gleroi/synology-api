using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synology.Api
{
    internal class Error : IError
    {
        public int Code { get; internal set; }
        public IEnumerable<dynamic> Errors { get; set; }
    }
}

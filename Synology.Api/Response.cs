using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Synology.Api
{
    internal class Response : IResponse
    {
        public bool Success { get; set; }
        public IDictionary<string, JToken> Data { get; set; }
        public Error Error { get; set; }
    }
}

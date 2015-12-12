using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Synology.Api
{
    internal class Response : IResponse
    {
        public bool Success { get; private set; }
        public IDictionary<string, JToken> Data { get; private set; }
        public Error Error { get; private set; }
    }
}
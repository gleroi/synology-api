using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Synology.Api
{
    public class ApiDescriptor
    {
        public string ApiName { get; internal set; }
        public string CgiPath { get; internal set; }
        public int MinVersion { get; internal set; }
        public int MaxVersion { get; internal set; }

        internal static ApiDescriptor MapFromDynamic(KeyValuePair<string, JToken> pair)
        {
            var api = pair.Value;

            return new ApiDescriptor
            {
                ApiName = pair.Key,
                CgiPath = api.Value<string>("path"),
                MinVersion = api.Value<int>("minVersion"),
                MaxVersion = api.Value<int>("maxVersion")
            };
        }
    }
}

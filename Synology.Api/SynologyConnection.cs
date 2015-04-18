using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Http;

namespace Synology.Api
{
    public class SynologyConnection
    {
        readonly HttpGateway Http;

        dynamic ApiDescription;

        public SynologyConnection(HttpGateway http)
        {
            this.Http = http;
            Init();
        }

        private async void Init()
        {
            var content = await this.Http.Get(this.MakeApiUrl("/webapi/query.cgi", "api=SYNO.API.Info&version=1&method=query&query=all"));
            this.ApiDescription = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
        }

        private string MakeApiUrl(string apiPath, string query)
        {
            UriBuilder uri = new UriBuilder("http", "nostromo.myds.me", 5000);
            uri.Path = apiPath;
            uri.Query = query;
            return uri.ToString();
        }
    }
}

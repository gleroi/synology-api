using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Synology.Api.Http;

namespace Synology.Api
{
    public class Synology
    {
        readonly HttpGateway Http;

        IEnumerable<ApiDescriptor> ApiDescription { get; set; }

        public Synology(HttpGateway http)
        {
            this.Http = http;
            Init();
        }

        private async void Init()
        {
            this.ApiDescription = await this.QueryInfo("all");
        }

        private string MakeApiUrl(string apiPath, string query)
        {
            UriBuilder uri = new UriBuilder("http", "nostromo.myds.me", 5000);
            uri.Path = "/webapi/" + apiPath;
            uri.Query = query;
            return uri.ToString();
        }

        public async Task<IResponse> SendRequest(string api, string method, IDictionary<string, string> parameters)
        {
            var apiCall = "api=" + api + "&method=" + method + "&version=1";
            var queryParams = String.Join("\\", parameters
                .Select(EscapeCharacter)
                .Select(pair => pair.Key + "=" + pair.Value));

            var url = this.MakeApiUrl("query.cgi", apiCall + "&" + queryParams);
            var json = await this.Http.Get(url);
            var result = JsonConvert.DeserializeObject<Response>(json);
            return result;
        }

        private KeyValuePair<string, string> EscapeCharacter(KeyValuePair<string, string> arg)
        {
            return arg;
        }

        public async Task<IEnumerable<ApiDescriptor>> QueryInfo(string query)
        {
            var response = await this.SendRequest("SYNO.API.Info", "query", new Dictionary<string, string> {
                { "query", query }
            });

            if (response.Success)
            {
                return response.Data.Select(ApiDescriptor.MapFromDynamic);
            }
            throw new SynologyApiException("Call to SINO.Api.Info failed", response.Error);
        }
    }
}

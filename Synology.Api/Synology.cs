using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Synology.Api.Http;

namespace Synology.Api
{
    public class Synology
    {
        readonly HttpGateway Http;

        Dictionary<string, ApiDescriptor> ApiDescription { get; set; }

        public Synology(HttpGateway http)
        {
            JsonConvert.DefaultSettings = Settings;

            this.Http = http;
            Initialize();
        }

        private void Initialize()
        {
            var task = this.QueryInfo("all");
            task.Wait();
            this.ApiDescription = task.Result.ToDictionary(desc => desc.ApiName);
        }

        static JsonSerializerSettings Settings()
        {
            return new JsonSerializerSettings()
            {
                ContractResolver = new PrivateSetterResolver()
            };
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
            var apiCall = "api=" + api + "&method=" + method;
            var queryParams = String.Join("&", parameters
                .Select(EscapeCharacter)
                .Select(pair => pair.Key + "=" + pair.Value));

            string url = null;
            if (this.ApiDescription == null && api == "SYNO.API.Info")
            {
                url = this.MakeApiUrl("query.cgi", apiCall +  "&version=1" + "&" + queryParams);
            }

            ApiDescriptor apiDesc = null;
            if (this.ApiDescription != null && this.ApiDescription.TryGetValue(api, out apiDesc))
            {
                url = this.MakeApiUrl(apiDesc.CgiPath, apiCall + "&version=" + apiDesc.MaxVersion + "&" + queryParams);
            }

            if (url != null)
            {
                var json = await this.Http.Get(url);
                var result = JsonConvert.DeserializeObject<Response>(json);
                return result;
            }
            throw new ArgumentException(api + " is an unknown api", "api");
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

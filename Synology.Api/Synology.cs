using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Synology.Api.Http;

namespace Synology.Api
{
    public class Synology
    {
        readonly IHttpGateway Http;

        Dictionary<string, ApiDescriptor> ApiDescription { get; set; }

        public Synology(IHttpGateway http)
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

        public async Task<IResponse> SendRequest(string api, string method, params Parameter[] parameters)
        {
            string apiCall;
            string queryParams;
            string apiPath;

            BuildApiUrl(api, method, parameters, out apiCall, out queryParams, out apiPath);

            var json = await this.Http.Get(apiPath, apiCall + "&" + queryParams);
            var result = JsonConvert.DeserializeObject<Response>(json);
            return result;
        }

        private void BuildApiUrl(string api, string method, Parameter[] parameters, out string apiPath, out string apiCall, out string queryParams)
        {
            apiCall = "api=" + api + "&method=" + method;
            queryParams = Parameter.Join("&", parameters);

            if (this.ApiDescription == null && api == "SYNO.API.Info")
            {
                apiPath = "query.cgi";
                apiCall += "&version=1";
            }
            else
            {
                ApiDescriptor apiDesc = null;
                if (this.ApiDescription != null && this.ApiDescription.TryGetValue(api, out apiDesc))
                {
                    apiPath = apiDesc.CgiPath;
                    apiCall += "&version=" + apiDesc.MaxVersion;
                }
            }
            throw new ArgumentException(api + " is an unknown api", "api");
        }

        protected Task<IResponse> PostRequest(string api, string method, params Parameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApiDescriptor>> QueryInfo(string query)
        {
            var response = await this.SendRequest("SYNO.API.Info", "query", new Parameter("query", query));

            if (response.Success)
            {
                return response.Data.Select(ApiDescriptor.MapFromDynamic);
            }
            throw new SynologyApiException("Call to SINO.Api.Info failed", response.Error);
        }

        public async Task<AuthResponse> Login(string account, string password, string session, string format = "cookie")
        {
            var response = await this.SendRequest("SYNO.API.Auth", "login",
                new Parameter("account", account),
                new Parameter("passwd", password),
                new Parameter("session", session),
                new Parameter("format", format));

            Sid? sid = null;
            if (response.Success && format == "sid" && response.Data != null) 
            {
                JToken token = null;
                if (response.Data.TryGetValue("sid", out token)) 
                {
                    sid = new Sid(token.Value<string>());
                }
            }
            return new AuthResponse(response.Success, sid, response.Error);
        }
    }
}

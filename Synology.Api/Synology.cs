using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synology.Api.Http;

namespace Synology.Api
{
    public class Synology
    {
        private readonly IHttpGateway Http;

        private Dictionary<string, ApiDescriptor> ApiDescription { get; set; }

        public Synology(IHttpGateway http)
        {
            JsonConvert.DefaultSettings = Settings;

            Http = http;
            Initialize();
        }

        private void Initialize()
        {
            var task = QueryInfo("all");
            task.Wait();
            ApiDescription = task.Result.ToDictionary(desc => desc.ApiName);
        }

        private static JsonSerializerSettings Settings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterResolver()
            };
        }

        private void BuildApiUrl(string api, string method, Parameter[] parameters, out string apiPath,
            out string apiCall, out string queryParams)
        {
            apiCall = "api=" + api + "&method=" + method;
            queryParams = Parameter.Join("&", parameters);

            if (ApiDescription == null && api == "SYNO.API.Info")
            {
                apiPath = "query.cgi";
                apiCall += "&version=1";
                return;
            }
            ApiDescriptor apiDesc = null;
            if (ApiDescription != null && ApiDescription.TryGetValue(api, out apiDesc))
            {
                apiPath = apiDesc.CgiPath;
                apiCall += "&version=" + apiDesc.MaxVersion;
                return;
            }
            throw new ArgumentException(api + " is an unknown api", "api");
        }

        public async Task<IResponse> SendRequest(string api, string method, params Parameter[] parameters)
        {
            string apiCall;
            string queryParams;
            string apiPath;

            BuildApiUrl(api, method, parameters, out apiPath, out apiCall, out queryParams);

            var json = await Http.Get(apiPath, apiCall + "&" + queryParams);
            var result = JsonConvert.DeserializeObject<Response>(json);
            return result;
        }

        protected async Task<IResponse> PostRequest(string api, string method, params Parameter[] parameters)
        {
            string apiCall;
            string queryParams;
            string apiPath;

            Parameter? file = parameters.FirstOrDefault(p => p.Key == "file");
            var others = parameters.Where(p => p.Key != "file").ToArray();

            BuildApiUrl(api, method, others, out apiPath, out apiCall, out queryParams);

            var json =
                await
                    Http.PostFile(apiPath, apiCall + "&" + queryParams,
                        file.HasValue ? file.Value.ValuesAsString() : null);
            var result = JsonConvert.DeserializeObject<Response>(json);
            return result;
        }

        public async Task<IEnumerable<ApiDescriptor>> QueryInfo(string query)
        {
            var response = await SendRequest("SYNO.API.Info", "query", new Parameter("query", query));

            if (response.Success)
            {
                return response.Data.Select(ApiDescriptor.MapFromDynamic);
            }
            throw new SynologyApiException("Call to SINO.Api.Info failed", response.Error);
        }

        public async Task<AuthResponse> Login(string account, string password, string session, string format = "cookie")
        {
            var response = await SendRequest("SYNO.API.Auth", "login",
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
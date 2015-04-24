﻿using System;
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



        public async Task<IResponse> SendRequest(string api, string method, params Parameter[] parameters)
        {
            var apiCall = "api=" + api + "&method=" + method;
            var queryParams = String.Join("&", parameters.Select(p => p.ToUrlParam()));

            string apiPath = null;
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

            if (apiPath != null)
            {
                var json = await this.Http.Get(apiPath, apiCall + "&" + queryParams);
                var result = JsonConvert.DeserializeObject<Response>(json);
                return result;
            }
            throw new ArgumentException(api + " is an unknown api", "api");
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
    }
}

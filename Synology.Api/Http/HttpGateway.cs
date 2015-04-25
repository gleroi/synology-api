using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Synology.Api.Http
{
    internal class HttpGateway : IHttpGateway
    {
        readonly string Scheme;
        readonly string Host;
        readonly int Port;

        public HttpGateway(string scheme, string host, int port)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.Port = port;
        }

        readonly HttpClient http = new HttpClient();

        private string MakeApiUrl(string apiPath, string query)
        {
            UriBuilder uri = new UriBuilder(this.Scheme, this.Host, this.Port);
            uri.Path = "/webapi/" + apiPath;
            uri.Query = query;
            return uri.ToString();
        }

        public async Task<string> Get(string apiPath, string query)
        {
            var url = this.MakeApiUrl(apiPath, query);
            var response = await this.http.GetAsync(url);
            var content = await response.EnsureSuccessStatusCode()
                .Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(content, 0, content.Length);
        }
    }
}

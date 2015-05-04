using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Files;

namespace Synology.Api.Http
{
    internal class HttpGateway : IHttpGateway
    {
        readonly string Scheme;
        readonly string Host;
        readonly int Port;

        public HttpGateway(IFileGateway fileGateway,
            string scheme, string host, int port)
        {
            this.filesGateway = fileGateway;
            this.Scheme = scheme;
            this.Host = host;
            this.Port = port;
        }

        readonly HttpClient http = new HttpClient();
        readonly IFileGateway filesGateway;

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

        public async Task<string> PostFile(string apiPath, string query, string filepath)
        {
            HttpResponseMessage response = null;
            var url = this.MakeApiUrl(apiPath, query);
            if (!String.IsNullOrEmpty(filepath))
            {
                var form = new MultipartFormDataContent();
                var stream = this.filesGateway.GetReadStream(filepath);
                var file = new StreamContent(stream);
                file.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(filepath),
                    Name = "file",
                };
                form.Add(file);
                response = await this.http.PostAsync(url, form);
            }
            else
            {
                response = await this.http.PostAsync(url, null);
            }
            var content = await response.EnsureSuccessStatusCode()
                .Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(content, 0, content.Length);
        }
    }
}

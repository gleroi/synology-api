using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Files;

namespace Synology.Api.Http
{
    internal class HttpGateway : IHttpGateway
    {
        private readonly string Scheme;
        private readonly string Host;
        private readonly int Port;

        public HttpGateway(IFileGateway fileGateway,
            string scheme, string host, int port)
        {
            filesGateway = fileGateway;
            Scheme = scheme;
            Host = host;
            Port = port;
        }

        private readonly HttpClient http = new HttpClient();
        private readonly IFileGateway filesGateway;

        private string MakeApiUrl(string apiPath, string query)
        {
            var uri = new UriBuilder(Scheme, Host, Port);
            uri.Path = "/webapi/" + apiPath;
            uri.Query = query;
            return uri.ToString();
        }

        public async Task<string> Get(string apiPath, string query)
        {
            var url = MakeApiUrl(apiPath, query);
            var response = await http.GetAsync(url);
            var content = await response.EnsureSuccessStatusCode()
                .Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(content, 0, content.Length);
        }

        public async Task<string> PostFile(string apiPath, string query, string filepath)
        {
            HttpResponseMessage response = null;
            var url = MakeApiUrl(apiPath, query);
            if (!string.IsNullOrEmpty(filepath))
            {
                var form = new MultipartFormDataContent();
                var stream = filesGateway.GetReadStream(filepath);
                var file = new StreamContent(stream);
                file.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(filepath),
                    Name = "file"
                };
                form.Add(file);
                response = await http.PostAsync(url, form);
            }
            else
            {
                response = await http.PostAsync(url, null);
            }
            var content = await response.EnsureSuccessStatusCode()
                .Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(content, 0, content.Length);
        }
    }
}
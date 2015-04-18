using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Synology.Api.Http
{
    public class HttpGateway
    {
        readonly HttpClient http = new HttpClient();

        public async Task<string> Get(string url)
        {
            var response = await this.http.GetAsync(url);
            var content = await response.EnsureSuccessStatusCode()
                .Content.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(content, 0, content.Length);
        }
    }
}

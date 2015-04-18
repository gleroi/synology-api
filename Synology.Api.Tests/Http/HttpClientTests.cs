using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Http;
using Xunit;

namespace Synology.Api.Tests.Http
{
    public class HttpClientTests
    {
        [Fact]
        public async void HttpGet_ShouldWork()
        {
            var http = new HttpGateway();

            var result = await http.Get(SynoConfig.WebApiInfo);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}

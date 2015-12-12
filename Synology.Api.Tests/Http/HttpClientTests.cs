using Xunit;

namespace Synology.Api.Tests.Http
{
    public class HttpClientTests
    {
        [Fact]
        public async void HttpGet_ShouldWork()
        {
            var http = TestConfig.HttpGateway();

            var result = await http.Get("query.cgi", "api=SYNO.API.Info&version=1&method=query&query=all");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
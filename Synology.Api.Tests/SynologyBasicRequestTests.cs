using Xunit;

namespace Synology.Api.Tests
{
    public class SynologyBasicRequestTests
    {
        private readonly Synology syno;

        public SynologyBasicRequestTests()
        {
            syno = new Synology(TestConfig.HttpGateway());
        }

        [Fact]
        public void Instantation_ShouldSucceed()
        {
            Assert.NotNull(syno);
        }

        [Fact]
        public async void CallingApiInfo_ShouldSucceed()
        {
            var result = await syno.SendRequest("SYNO.API.Info", "query", new Parameter("query", "all"));
            CheckResponse.HasSucceeded(result);
        }

        [Fact]
        public async void CallingApiInfo_ShouldReturnAResponse()
        {
            var result = await syno.SendRequest("SYNO.API.Info", "query", new Parameter("query", "all"));

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IResponse>(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
        }

        [Fact]
        public async void CallApiInfo_WithMultiValueParameter_ShouldReturnAResponse()
        {
            var result = await syno.SendRequest("SYNO.API.Info", "query",
                new Parameter("query", "SYNO.API.Info", "SYNO.API.Auth"));

            CheckResponse.HasSucceeded(result);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Contains("SYNO.API.Info", result.Data.Keys);
            Assert.Contains("SYNO.API.Auth", result.Data.Keys);
        }

        [Fact]
        public async void CallingApi_ShouldFindCgiPath()
        {
            var result = await syno.SendRequest("SYNO.API.Auth", "login",
                new Parameter("account", "tmp"),
                new Parameter("passwd", "tmp"));

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(402, result.Error.Code);
            Assert.Null(result.Data);
        }
    }
}
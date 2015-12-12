using Xunit;

namespace Synology.Api.Tests.Auth
{
    public class ApiAuthTests
    {
        private readonly Synology syno;

        private const string session = "FileStation";

        public ApiAuthTests()
        {
            syno = new Synology(TestConfig.HttpGateway());
        }

        [Fact]
        public async void WhenLoginWithValidCredentials_Sid_ShouldGetSuccessAndSid()
        {
            var response = await syno.Login(TestConfig.Account, TestConfig.Password, session, "sid");

            CheckResponse.HasSucceeded(response);
            Assert.NotNull(response.Sid);
        }

        [Fact]
        public async void WhenLoginWithValidCredentials_Cookie_ShouldGetSuccessAndCookie()
        {
            var response = await syno.Login(TestConfig.Account, TestConfig.Password, session);

            CheckResponse.HasSucceeded(response);
            Assert.Null(response.Sid);
        }
    }
}
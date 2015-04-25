using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Http;
using Xunit;

namespace Synology.Api.Tests.Auth
{
    public class ApiAuthTests
    {
        readonly Synology syno;

        public ApiAuthTests()
        {
            this.syno = new Synology(new HttpGateway());
        }

        [Fact]
        public async void WhenLoginWithValidCredentials_Sid_ShouldGetSuccessAndSid()
        {
            var response = await syno.Login("", "", "", "sid");

            CheckResponse.HasSucceeded(response);
            Assert.NotEmpty(response.Sid);
        }

        [Fact]
        public async void WhenLoginWithValidCredentials_Cookie_ShouldGetSuccessAndCookie()
        {
            var response = await syno.Login("", "", "");

            CheckResponse.HasSucceeded(response);
            Assert.Null(response.Sid);
        }
    }
}

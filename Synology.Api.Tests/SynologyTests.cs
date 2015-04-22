using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Synology.Api.Http;
using Xunit;

namespace Synology.Api.Tests
{
    public class SynologyTests
    {
        readonly Synology syno;

        public SynologyTests()
        {
            this.syno = new Synology(new HttpGateway());
        }

        [Fact]
        public void Instantation_ShouldSucceed()
        {
            Assert.NotNull(syno);
        }

        [Fact]
        public async void CallingApiInfo_ShouldSucceed()
        {
            var result = await syno.SendRequest("SYNO.API.Info", "query", new Dictionary<string,string> {
                { "query", "all" }});
            Assert.NotNull(result);
        }

        [Fact]
        public async void CallingApiInfo_ShouldReturnAResponse()
        {
            var result = await syno.SendRequest("SYNO.API.Info", "query", new Dictionary<string, string> {
                { "query", "all" }});

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IResponse>(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
        }

        [Fact]
        public async void CallingApi_ShouldFindCgiPath()
        {
            var result = await syno.SendRequest("SYNO.API.Auth", "login", new Dictionary<string, string>
            {
                { "account", "tmp" },
                { "passwd", "tmp" },
            });

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(402, result.Error.Code);
            Assert.Null(result.Data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //[Fact]
        //public async void CallingApi_ShouldFindCgiPath()
        //{

        //}
    }
}

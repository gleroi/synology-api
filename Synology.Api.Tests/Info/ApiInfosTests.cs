using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Http;
using Xunit;

namespace Synology.Api.Tests.Info
{
    public class ApiInfosTests
    {
        readonly Synology syno;

        public ApiInfosTests()
        {
            this.syno = new Synology(new HttpGateway());
        }

        [Fact]
        public async void CallQueryInfo_ShouldReturnApiDescriptor() 
        {
            var descriptors = await syno.QueryInfo("all");

            Assert.NotNull(descriptors);
            Assert.NotEmpty(descriptors);

            Assert.All(descriptors, desc => Assert.NotEmpty(desc.ApiName));
            Assert.All(descriptors, desc => Assert.NotEmpty(desc.CgiPath));
            Assert.All(descriptors, desc => Assert.NotEqual(0, desc.MinVersion));
            Assert.All(descriptors, desc => Assert.NotEqual(0, desc.MaxVersion));
        }
    }
}

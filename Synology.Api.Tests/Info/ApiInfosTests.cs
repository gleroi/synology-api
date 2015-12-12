using Xunit;

namespace Synology.Api.Tests.Info
{
    public class ApiInfosTests
    {
        private readonly Synology syno;

        public ApiInfosTests()
        {
            syno = new Synology(TestConfig.HttpGateway());
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
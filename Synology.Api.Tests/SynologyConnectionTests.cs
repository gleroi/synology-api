using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Synology.Api.Tests
{
    public class SynologyConnectionTests
    {
        [Fact]
        public void Instantation_ShouldSucceed()
        {
            var conn = new SynologyConnection(new Api.Http.HttpGateway());

            Assert.NotNull(conn);
        }
    }
}

using Synology.Api.Http;
using Synology.Net45;

namespace Synology.Api.Tests
{
    internal static class TestConfig
    {
        internal static IHttpGateway HttpGateway()
        {
            return new HttpGateway(new FileGateway(),
                "http", "", 5000);
        }

        public static string Account = "";
        public static string Password = "";
    }
}
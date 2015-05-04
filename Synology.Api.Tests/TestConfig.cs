using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Http;
using Synology.Net45;

namespace Synology.Api.Tests
{
    static class TestConfig
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

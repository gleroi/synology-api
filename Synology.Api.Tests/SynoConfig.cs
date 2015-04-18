using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synology.Api.Tests
{
    class SynoConfig
    {
        public const string Host = "nostromo.myds.me";
        public const string Port = "5000";

        public const string Url = "http://" + Host + ":" + Port;

        public const string WebApiInfo = Url + "/webapi/query.cgi?api=SYNO.API.Info&version=1&method=query&query=all";
    }
}

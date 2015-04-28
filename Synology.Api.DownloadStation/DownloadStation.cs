using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Http;

namespace Synology.Api.Download
{
    public class DownloadStation : Synology
    {
        public const string SessionName = "DownloadStation";

        public DownloadStation(IHttpGateway http)
            : base(http)
        {
        }

        public async Task<TaskListResponse> ListTasks(string sid, int offset = 0, int limit = -1, params string[] additionals)
        {
            var result = await this.SendRequest("SYNO.DownloadStation.Task", "list",
                new Parameter("offset", offset),
                new Parameter("limit", limit),
                new Parameter("_sid", sid),
                new Parameter("additional", additionals));

            var response = new TaskListResponse(result.Success, result.Data, result.Error);
            return response;
        }

        public async Task<IResponse> CreateTaskUri(string sid, string uri, string destination)
        {
            var result = await this.SendRequest("SYNO.DownloadStation.Task", "create",
                new Parameter("uri", uri),
                new Parameter("destination", destination),
                new Parameter("_sid", sid));

            return result;
        }

        public async Task<IResponse> CreateTaskFile(string sid, string filepath, string destination)
        {
            return null;
        }

    }
}

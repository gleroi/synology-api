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

        public async Task<TaskListResponse> ListTasks(Sid sid, int offset = 0, int limit = -1, params string[] additionals)
        {
            var result = await this.SendRequest("SYNO.DownloadStation.Task", "list",
                new Parameter("offset", offset),
                new Parameter("limit", limit),
                new Parameter("_sid", sid.Value),
                new Parameter("additional", additionals));

            var response = new TaskListResponse(result.Success, result.Data, result.Error);
            return response;
        }

        public async Task<IResponse> CreateTaskUri(Sid sid, string uri, string destination)
        {
            var result = await this.SendRequest("SYNO.DownloadStation.Task", "create",
                new Parameter("uri", uri),
                new Parameter("destination", destination),
                new Parameter("_sid", sid.Value));

            return result;
        }

        public async Task<IResponse> CreateTaskFile(Sid sid, string filepath, string destination)
        {
            var result = await this.PostRequest("SYNO.DownloadStation.Task", "create",
                new Parameter("destination", destination),
                new Parameter("_sid", sid.Value),
                new Parameter("file", filepath));
            return result;
        }

    }
}

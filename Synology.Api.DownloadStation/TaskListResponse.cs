using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Synology.Api.Download
{
    public class TaskListResponse : IResponseStatus
    {
        private bool p;

        internal TaskListResponse(bool success, IDictionary<string, JToken> data, Error error)
        {
            Success = success;
            Error = error;
            Data = TaskList.Create(data);
        }

        public Error Error { get; private set; }
        public bool Success { get; private set; }

        public TaskList Data { get; private set; }
    }
}
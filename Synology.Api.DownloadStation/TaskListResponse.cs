using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Synology.Api.Download
{
    public class TaskListResponse : IResponseStatus
    {
        private bool p;

        internal TaskListResponse(bool success, IDictionary<string, JToken> data, Error error)
        {
            this.Success = success;
            this.Error = error;
            this.Data = TaskList.Create(data);
        }

        public Error Error { get; private set; }
        public bool Success { get; private set; }

        public TaskList Data { get; private set; }
    }
}

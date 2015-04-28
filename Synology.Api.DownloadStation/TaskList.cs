using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Synology.Api.Download
{
    public class TaskList
    {
        public int Offset { get; private set; }
        public int Total { get; private set; }
        public IEnumerable<DsTask> Tasks { get; private set; }

        internal TaskList()
        {

        }

        internal static TaskList Create(IDictionary<string, JToken> data)
        {
            var list = new TaskList();

            if (data != null)
            {
                list.Offset = data["offeset"].Value<int>();
                list.Total = data["total"].Value<int>();
                list.Tasks = data["tasks"].ToList().Select(DsTask.Create).ToList();
            }
            return list;
        }
    }
}

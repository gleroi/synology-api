using Newtonsoft.Json.Linq;

namespace Synology.Api.Download
{
    public class DsTask
    {
        public string Id { get; private set; }
        public string Type { get; private set; }
        public string Username { get; private set; }
        public string Title { get; private set; }
        public long Size { get; private set; }
        public string Status { get; private set; }
        public string StatusExtra { get; private set; }

        internal static DsTask Create(JToken token)
        {
            var task = new DsTask();
            task.Id = token.Value<string>("id");
            task.Type = token.Value<string>("type");
            task.Username = token.Value<string>("username");
            task.Title = token.Value<string>("title");
            task.Size = token.Value<long>("size");
            task.Status = token.Value<string>("status");
            task.StatusExtra = token.Value<string>("status_extra");
            return task;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Download;
using Xunit;

namespace Synology.Api.Tests.Download
{
    public class TaskListTests
    {
        readonly DownloadStation station;
        public TaskListTests()
        {
            this.station = new DownloadStation(TestConfig.HttpGateway());
        }

        [Fact]
        public async void TaskList_ShouldReturnsCurrentTask() 
        {
            var auth = await station.Login(TestConfig.Account, TestConfig.Password, DownloadStation.SessionName, "sid");

            CheckResponse.HasSucceeded(auth);
            var sid = auth.Sid;
            Assert.NotNull(sid);

            var tasks = await station.ListTasks(sid.Value);
            CheckResponse.HasSucceeded(tasks);
            Assert.NotNull(tasks.Data);
            Assert.NotEmpty(tasks.Data.Tasks);
        }
    }
}

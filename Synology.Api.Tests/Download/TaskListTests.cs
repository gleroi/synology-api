﻿using Synology.Api.Download;
using Xunit;

namespace Synology.Api.Tests.Download
{
    public class TaskListTests
    {
        private readonly DownloadStation station;

        public TaskListTests()
        {
            station = new DownloadStation(TestConfig.HttpGateway());
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
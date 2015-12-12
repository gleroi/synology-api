using Synology.Api.Download;
using Xunit;

namespace Synology.Api.Tests.Download
{
    public class TaskCreateTests
    {
        private readonly DownloadStation station;

        public TaskCreateTests()
        {
            station = new DownloadStation(TestConfig.HttpGateway());
        }

        [Fact]
        public async void TaskCreate_WithUrl_ShouldReturnsTrue()
        {
            var auth = await station.Login(TestConfig.Account, TestConfig.Password, DownloadStation.SessionName, "sid");

            CheckResponse.HasSucceeded(auth);
            var sid = auth.Sid;
            Assert.NotNull(sid);

            var added =
                await
                    station.CreateTaskUri(sid.Value, "http://www.omgtorrent.com/clic_dl.php?id=23642", "telechargements");
            CheckResponse.HasSucceeded(added);
        }

        [Fact]
        public async void TaskCreate_WithFile_ShouldReturnsTrue()
        {
            var auth = await station.Login(TestConfig.Account, TestConfig.Password, DownloadStation.SessionName, "sid");

            CheckResponse.HasSucceeded(auth);
            var sid = auth.Sid;
            Assert.NotNull(sid);

            var added = await station.CreateTaskFile(sid.Value, "Resources/test.torrent", "telechargements");
            CheckResponse.HasSucceeded(added);
        }
    }
}
using System.IO;

namespace Synology.Api.Files
{
    public interface IFileGateway
    {
        Stream GetReadStream(string file);
    }
}
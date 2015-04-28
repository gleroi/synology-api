using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synology.Api.Files;

namespace Synology.Net45
{
    public class FileGateway : IFileGateway
    {
        
        public Stream GetReadStream(string file)
        {
            var info = new FileInfo(file);
            if (info.Exists)
            {
                return new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            return null;
        }
    }
}

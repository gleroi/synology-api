using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synology.Api.Files
{
    public interface IFileGateway
    {
        Stream GetReadStream(string file);
    }
}

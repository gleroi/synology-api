using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synology.Api
{
    public interface IResponseStatus
    {
        Error Error { get; }
        bool Success { get; }
    }
}

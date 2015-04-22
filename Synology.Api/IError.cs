using System;
using System.Collections.Generic;

namespace Synology.Api
{
    public interface IError
    {
        int Code { get; }
        IEnumerable<dynamic> Errors { get; }
    }
}

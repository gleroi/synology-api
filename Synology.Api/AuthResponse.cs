using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synology.Api
{
    public class AuthResponse : IResponseStatus
    {
        public Error Error { get; private set; }
        public bool Success { get; private set; }
        public Sid? Sid { get; private set; }

        public AuthResponse(bool success, Sid? sid, Error error)
        {
            this.Success = success;
            this.Error = error;
            this.Sid = sid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synology.Api
{
    class SynologyApiException : Exception
    {
        public Error Error { get; private set; }

        public SynologyApiException(string message, Error error)
            : base(message)
        {
            this.Error = error;
        }

    }
}

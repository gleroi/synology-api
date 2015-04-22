using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synology.Api
{
    class SynologyApiException : Exception
    {
        public IError Error { get; private set; }

        public SynologyApiException(string message, IError error)
            : base(message)
        {
            this.Error = error;
        }

    }
}

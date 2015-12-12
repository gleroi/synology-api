using System;

namespace Synology.Api
{
    internal class SynologyApiException : Exception
    {
        public Error Error { get; private set; }

        public SynologyApiException(string message, Error error)
            : base(message)
        {
            Error = error;
        }
    }
}
namespace Synology.Api
{
    public class AuthResponse : IResponseStatus
    {
        public Error Error { get; private set; }
        public bool Success { get; private set; }
        public Sid? Sid { get; private set; }

        public AuthResponse(bool success, Sid? sid, Error error)
        {
            Success = success;
            Error = error;
            Sid = sid;
        }
    }
}
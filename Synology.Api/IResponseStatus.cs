namespace Synology.Api
{
    public interface IResponseStatus
    {
        Error Error { get; }
        bool Success { get; }
    }
}
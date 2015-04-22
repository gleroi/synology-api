using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace Synology.Api
{
    public interface IResponse
    {
        IDictionary<string, JToken> Data { get; }
        IError Error { get; }
        bool Success { get; }
    }
}

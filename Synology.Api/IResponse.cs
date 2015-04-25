using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace Synology.Api
{
    public interface IResponse : IResponseStatus
    {
        IDictionary<string, JToken> Data { get; }
    }
}

﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Synology.Api
{
    public class Error
    {
        public int Code { get; private set; }
        public IEnumerable<JObject> Errors { get; private set; }
    }
}
using System;

namespace Synology.Api
{
    public struct Sid
    {
        public string Value { get; private set; }

        public Sid(string value)
            : this()
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value", "value cannot be null or empty");
            }
            Value = value;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonObjects.Utilities
{
    public class JsonWebTokenSettings
    {
        public JsonWebTokenSettings
        (
            string key,
            TimeSpan expires
        )
        {
            Key = key;
            Expires = expires;
        }

        public JsonWebTokenSettings
        (
            string key,
            TimeSpan expires,
            string audience,
            string issuer
        )
        : this(key, expires)
        {
            Audience = audience;
            Issuer = issuer;
        }

        public string Audience { get; private set; } = string.Empty;

        public TimeSpan Expires { get; }

        public string Issuer { get; } = string.Empty;

        public string Key { get; }
    }//end class
}

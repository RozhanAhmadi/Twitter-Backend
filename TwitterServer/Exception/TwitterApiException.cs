using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Exceptions
{
    [Serializable]
    public class TwitterApiException : System.Exception
    {
        public int Code { get; protected set; }
        public TwitterApiException()
        {

        }
        public TwitterApiException(string message) : base(message)
        {

        }
        public TwitterApiException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}

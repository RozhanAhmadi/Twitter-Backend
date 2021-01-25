using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models
{
    public class TwitterApiException : System.Exception
    {
        public TwitterApiException(string message) : base(message)
        {

        }
    }
}

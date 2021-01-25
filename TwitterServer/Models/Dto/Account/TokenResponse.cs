using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Dto.Account
{
    public class TokenResponse
    {
        public TokenResponse(string token, int expireIn)
        {
            Token = token;
            ExpireIn = expireIn;
            Succeeded = true;
        }
        public string Token { get; }
        public int ExpireIn { get; set; }
        public bool Succeeded { get; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.Account;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Commands.UserCommands
{
    public interface ISignInUserCommand
    {
        public Task<TokenResponse> SignInUserHandler(SignInUserDto request);
    }
}

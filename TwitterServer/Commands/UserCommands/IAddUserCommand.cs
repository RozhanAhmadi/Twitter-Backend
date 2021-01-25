using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Commands.UserCommands
{
    public interface IAddUserCommand
    {
        public Task AddUser(AddUserDto request);
    }
}

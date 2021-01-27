using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Commands.UserCommands.Interfaces
{
    public interface IAddUserCommand
    {
        public Task AddUserHandler(AddUserDto request);
    }
}

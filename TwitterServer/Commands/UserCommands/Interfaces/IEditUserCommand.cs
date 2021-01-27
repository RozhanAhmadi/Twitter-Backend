using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Commands.UserCommands.Interfaces
{
    public interface IEditUserCommand
    {
        public Task EditUserHandler(EditUserDto request);
    }
}

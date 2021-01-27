using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Commands.UserCommands.Interfaces
{
    public interface IGetUserCommand
    {
        public Task<ResponseUserDto> GetUserByIdHandler(int id);
        public Task<List<ResponseUserDto>> GetUserByUsernameHandler(string username);
    }
}

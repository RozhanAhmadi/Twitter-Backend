using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Commands.UserCommands;
using TwitterServer.Models.Dto.Account;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAddUserCommand _addUserCommand;
        private readonly ISignInUserCommand _iSignInUserCommand;
        private readonly IEditUserCommand _ieditUserCommand;
        private readonly IGetUserCommand _iGetUserCommand;

        public UserController(IAddUserCommand iAddUserCommand 
                              ,ISignInUserCommand iSignInUserCommand
                                ,IEditUserCommand iEditUserCommand
                                , IGetUserCommand iGetUserCommand)
        {
            _addUserCommand = iAddUserCommand;
            _iSignInUserCommand = iSignInUserCommand;
            _ieditUserCommand = iEditUserCommand;
            _iGetUserCommand = iGetUserCommand;
        }

        [HttpPost]
        public async Task AddUser(AddUserDto request)
        {
            await _addUserCommand.AddUserHandler(request);
        }

        [HttpPost("signIn")]
        public async Task<TokenResponse> SignInUser(SignInUserDto request)
        {
            return  await _iSignInUserCommand.SignInUserHandler(request);
        }
        [Authorize]
        [HttpPut()]
        public async Task EditUser(EditUserDto request)
        {
            await _ieditUserCommand.EditUserHandler(request);
        }

        [HttpGet("username/{username}")]
        public async Task<ResponseUserDto> GetUserByUsername(string username)
        {
           return await _iGetUserCommand.GetUserByUsernameHandler(username);
        }

        [HttpGet("{id}")]
        public async Task<ResponseUserDto> GetUserById(int id)
        {
           return await _iGetUserCommand.GetUserByIdHandler(id);
        }
    }
}

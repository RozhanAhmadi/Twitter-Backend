using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Commands.UserCommands;
using TwitterServer.Commands.UserCommands.Interfaces;
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
        private readonly IFollowUserCommand _iFollowUserCommand;

        public UserController(IAddUserCommand iAddUserCommand
                              , ISignInUserCommand iSignInUserCommand
                                , IEditUserCommand iEditUserCommand
                                , IGetUserCommand iGetUserCommand
                                , IFollowUserCommand iFollowUserCommand)
        {
            _addUserCommand = iAddUserCommand;
            _iSignInUserCommand = iSignInUserCommand;
            _ieditUserCommand = iEditUserCommand;
            _iGetUserCommand = iGetUserCommand;
            _iFollowUserCommand = iFollowUserCommand;
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
        public async Task<List<ResponseUserDto>> GetUserByUsername(string username)
        {
           return await _iGetUserCommand.GetUserByUsernameHandler(username);
        }

        [HttpGet("{id}")]
        public async Task<ResponseUserDto> GetUserById(int id)
        {
           return await _iGetUserCommand.GetUserByIdHandler(id);
        }

        [Authorize]
        [HttpPost("follow")]
        public async Task FollowUser(FollowUserDto request)
        {
            await _iFollowUserCommand.FollowUserHandler(request);
        }

        [Authorize]
        [HttpPost("unfollow")]
        public async Task UnfollowUser(FollowUserDto request)
        {
            await _iFollowUserCommand.UnfollowUserHandler(request);
        }
        
    }
}

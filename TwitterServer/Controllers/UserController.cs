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

        public UserController(IAddUserCommand addUserCommand , 
                              ISignInUserCommand iSignInUserCommand)
        {
            _addUserCommand = addUserCommand;
            _iSignInUserCommand = iSignInUserCommand;
        }

        [HttpPost]
        public async Task AddUser(AddUserDto request)
        {
            await _addUserCommand.AddUserHandler(request);
        }

        //[HttpPost("signIn")]
        //public async Task SignInUser(SignInUserDto request)
        //=> await _iSignInUserCommand.SignInUserHandler(request);
        

        //[HttpGet]
        //public async Task GetAll(AddUserDto request)
        //{
        //    await _addUserCommand.AddUser(request);
        //}
        //[HttpGet("{username}")]
        //public async Task GetAlsl(string username)
        //{
        //    await _addUserCommand.AddUser(request);
        //}
    }
}

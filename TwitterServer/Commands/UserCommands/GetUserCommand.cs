using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.Exceptions;
using TwitterServer.Models.Dto.UserDto;
using TwitterServer.Models.Entity;

namespace TwitterServer.Commands.UserCommands
{
    public class GetUserCommand : IGetUserCommand
    {
        private readonly AppDbContext _dbContext;

        public GetUserCommand(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseUserDto> GetUserByIdHandler(int id)
        {
            var user = await _dbContext.Users.Where(p => p.Id == id).Select(p =>
                  new ResponseUserDto()
                  {
                      Id = p.Id,
                      Username = p.Username,
                      Password = p.Password,
                      Email = p.Email,

                  }).SingleOrDefaultAsync();
            if(user is null)
                throw new TwitterApiException(400, "Invalid User id");

            return user;
        }

        public async Task<ResponseUserDto> GetUserByUsernameHandler(string username)
        {
            var user = await _dbContext.Users.Where(p => p.Username == username.ToLower()).Select(p =>
                 new ResponseUserDto()
                 {
                     Id = p.Id,
                     Username = p.Username,
                     Password = p.Password,
                     Email = p.Email,

                 }).SingleOrDefaultAsync();
            if (user is null)
                throw new TwitterApiException(400, "Invalid Username");

            return user;
        }

        
    }
}

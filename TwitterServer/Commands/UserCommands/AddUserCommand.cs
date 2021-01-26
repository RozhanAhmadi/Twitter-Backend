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
    public class AddUserCommand : IAddUserCommand
    {
        private readonly AppDbContext _dbContext;

        public AddUserCommand(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserHandler(AddUserDto request)
        {
            var user = await _dbContext.Users.AnyAsync(x => x.Username == request.Username.ToLower());
            var email = await _dbContext.Users.AnyAsync(x => x.Email == request.Email.ToLower());

            if (user)
                throw new TwitterApiException(400,"Username already exists");

            if (email)
                throw new TwitterApiException(400,"Email already exists");

            string usernamestr = request.Username;
            string emailstr = request.Email;

            if (usernamestr == string.Empty)
                usernamestr = emailstr.Substring(0, emailstr.IndexOf("@"));

            var userToSave = new UserEntity
            {
                Username = usernamestr.ToLower(),
                Email = request.Email.ToLower(),
                Password = request.Password.ToLower(),
            };

            await _dbContext.Users.AddAsync(userToSave);
            await _dbContext.SaveChangesAsync();
        }
    }
}

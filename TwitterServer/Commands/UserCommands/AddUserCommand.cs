using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.Models;
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

        public async Task AddUser(AddUserDto request)
        {
            var user = await _dbContext.Users.AnyAsync(x => x.Username == request.Username);
            if (user)
                throw new TwitterApiException("Username already exists!");

            string username = request.Username;
            string email = request.Email;

            if (username == string.Empty)
                username = email.Substring(0, email.IndexOf("@"));

            var userToSave = new UserEntity
            {
                Username = username,
                Email = request.Email,
                Password = request.Password,
            };

            await _dbContext.Users.AddAsync(userToSave);
            await _dbContext.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TwitterServer.Utilities;
using TwitterServer.Models.Dto.UserDto;
using Microsoft.EntityFrameworkCore;
using TwitterServer.Exceptions;
using TwitterServer.Models.Entity;
using Microsoft.AspNetCore.Http;

namespace TwitterServer.Commands.UserCommands
{
    public class EditUserCommand : IEditUserCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public EditUserCommand(AppDbContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        public async Task EditUserHandler(EditUserDto request)
        {

            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;

            var userInstance = await _dbContext.Users.AnyAsync(x => x.Username == request.Username.ToLower());
            if(userInstance)
                throw new TwitterApiException(400, "Username already exists");

            string nameToFind = user.Identity.Name.ToLower();
            var userToEdit = await _dbContext.Users.Where(p => p.Username == nameToFind).Select(p =>
                   new UserEntity()
                   {
                       Id = p.Id,
                       Username = p.Username,
                       Password = p.Password,
                       Email = p.Email,
                       UserFollowRelations = p.UserFollowRelations,

                   }).SingleOrDefaultAsync();

            userToEdit.Username = request.Username.ToLower();
            _dbContext.Users.Update(userToEdit);
            await _dbContext.SaveChangesAsync();
        }
    }
}

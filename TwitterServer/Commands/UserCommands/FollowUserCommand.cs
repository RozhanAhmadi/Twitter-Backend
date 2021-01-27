using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterServer.Commands.UserCommands.Interfaces;
using TwitterServer.Data;
using TwitterServer.Models.Dto.UserDto;
using TwitterServer.Models.Entity;
using TwitterServer.Utilities;

namespace TwitterServer.Commands.UserCommands
{
    public class FollowUserCommand : IFollowUserCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public FollowUserCommand(AppDbContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        public async Task FollowUserHandler(FollowUserDto request)
        {
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            string followerName = user.Identity.Name.ToLower();
            string followerID = ClaimExtensions.GetUserId(user);
            var following = await _dbContext.Users.Where(p => p.Username == request.Username.ToLower()).Select(p =>
                   new UserEntity()
                   {
                       Id = p.Id,
                       Username = p.Username,
                       Password = p.Password,
                       Email = p.Email,
                       UserFollowRelations = p.UserFollowRelations,

                   }).SingleOrDefaultAsync();
            var relation = new UserFollowRelationEntity();
            relation.FollowerId = int.Parse(followerID);
            relation.FollowingId = following.Id;
            await _dbContext.UserFollowRelations.AddAsync(relation);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UnfollowUserHandler(FollowUserDto request)
        {
            
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            string followerName = user.Identity.Name.ToLower();
            string followerID = ClaimExtensions.GetUserId(user);
            var following = await _dbContext.Users.Where(p => p.Username == request.Username.ToLower()).Select(p =>
                   new UserEntity()
                   {
                       Id = p.Id,
                       Username = p.Username,
                       Password = p.Password,
                       Email = p.Email,
                       UserFollowRelations = p.UserFollowRelations,

                   }).SingleOrDefaultAsync();
            var relation = await _dbContext.UserFollowRelations.Where(p => (p.FollowerId == int.Parse(followerID) && p.FollowingId==following.Id)).Select(p =>
                   new UserFollowRelationEntity()
                   {
                       Id = p.Id,
                       FollowerId = p.FollowerId,
                       FollowingId = p.FollowingId,

                   }).SingleOrDefaultAsync();

             _dbContext.UserFollowRelations.Remove(relation);
            await _dbContext.SaveChangesAsync();

        }

    }
}

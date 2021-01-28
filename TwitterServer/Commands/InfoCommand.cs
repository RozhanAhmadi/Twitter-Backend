using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.Models.Dto;
using TwitterServer.Models.Dto.HashtagDto;
using TwitterServer.Utilities;

namespace TwitterServer.Commands
{
    public class InfoCommand : IInfoCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public InfoCommand(AppDbContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _iHttpContextAccessor = iHttpContextAccessor;
        }        

        public async Task<List<ResponseHashtagDto>> GetTopHashtagsHandler()
        {
            var tags = await _dbContext.Hashtags.Select(p =>
                  new ResponseHashtagDto()
                  {
                      Id = p.Id,
                      Content = p.Content,
                      UsageCount = p.UsageCount,

                  }).ToListAsync();
            List<ResponseHashtagDto> SortedList = (List<ResponseHashtagDto>)tags.OrderByDescending(o => o.UsageCount).Take(10).ToList();
            return SortedList;
        }
        public async Task<List<ActivityLogDto>> GetHomeLogHandler()
        {
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            int userID = int.Parse(ClaimExtensions.GetUserId(user));

            var logs = await _dbContext.ActivityLogs.Where(p => p.TargetUserId == userID).Select(p =>
            new ActivityLogDto()
            {
                Id = p.Id,
                ActorId = p.ActorId,
                ActorName = p.ActorName,
                ActionTypeId = p.ActionTypeId,
                ActionTypeName = p.ActionTypeName,
                TargetTweetId = p.TargetTweetId,
                TargetUserId = p.TargetUserId,
                Date = p.Date,
            }).ToListAsync();

            List<ActivityLogDto> SortedList = (List<ActivityLogDto>)logs.OrderByDescending(o => o.Date).ToList();
            return SortedList;
        }

        public async Task<List<ActivityLogDto>> GetSelfLogHandler()
        {
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            int userID = int.Parse(ClaimExtensions.GetUserId(user));

            var logs = await _dbContext.ActivityLogs.Where(p => p.ActorId == userID).Select(p =>
            new ActivityLogDto()
            {
                Id = p.Id,
                ActorId = p.ActorId,
                ActorName = p.ActorName,
                ActionTypeId = p.ActionTypeId,
                ActionTypeName = p.ActionTypeName,
                TargetTweetId = p.TargetTweetId,
                TargetUserId = p.TargetUserId,
                Date = p.Date,
            }).ToListAsync();

            List<ActivityLogDto> SortedList = (List<ActivityLogDto>)logs.OrderByDescending(o => o.Date).ToList();
            return SortedList;
        }
    }
}

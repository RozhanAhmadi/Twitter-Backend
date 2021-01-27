using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterServer.Data;
using TwitterServer.Exceptions;
using TwitterServer.Models.Dto.HashtagDto;
using TwitterServer.Models.Dto.TweetDto;
using TwitterServer.Models.Entity;
using TwitterServer.Utilities;

namespace TwitterServer.Commands.TweetCommands
{
    public class GetTweetCommand : IGetTweetCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public GetTweetCommand(AppDbContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        public async Task<List<ResponseTweetDto>> GetHomeTweetsHandler()
        {
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            string userName = user.Identity.Name.ToLower();
            int userID = int.Parse(ClaimExtensions.GetUserId(user));
            var followings = await _dbContext.UserFollowRelations.AsNoTracking().Where(p => p.FollowerId == userID).Select(p =>
                   new UserFollowRelationEntity()
                   {
                       Id = p.Id,
                       FollowerId = p.FollowerId,
                       FollowingId = p.FollowingId,
                   }).ToListAsync();
            if (followings != null && followings.Count > 0)
            {
                var list = new List<ResponseTweetDto>();
                foreach (var userf in followings)
                {
                    var tweets = await _dbContext.UserTweetRelations.AsNoTracking().Where(p => p.UserId == userf.FollowingId).Select(p =>
                   new UserTweetRelationEntity()
                   {
                       Id = p.Id,
                       UserId = p.UserId,
                       TweetId = p.TweetId,
                   }).ToListAsync();

                    if (tweets != null && tweets.Count > 0)
                    {
                        foreach (var tweetf in tweets)
                        {
                            var tweettt = await _dbContext.Tweets.Where(p => p.Id == tweetf.TweetId).Select(p =>
                                new ResponseTweetDto()
                                {
                                    Id = p.Id,
                                    Content = p.Content,
                                    CreatedAt = p.CreatedAt,
                                    CreatorId = p.CreatorId,
                                    LikeCount = p.LikeCount,
                                    RetweetCount = p.RetweetCount,
                                    IsRetweet = p.IsRetweet,
                                }).SingleOrDefaultAsync();
                            list.Add(tweettt);
                        }
                    }
                    List<ResponseTweetDto> SortedList = list.OrderByDescending(o => o.CreatedAt).ToList();
                    return SortedList;
                }
            }
            return null;
        }

        public async Task<List<ResponseTweetDto>> GetSelfTweetsHandler()
        {
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            string userName = user.Identity.Name.ToLower();
            int userID = int.Parse(ClaimExtensions.GetUserId(user));
            var selfTweets = await _dbContext.Tweets.Where(p => p.CreatorId == userID).Select(p =>
                                new ResponseTweetDto()
                                {
                                    Id = p.Id,
                                    Content = p.Content,
                                    CreatedAt = p.CreatedAt,
                                    CreatorId = p.CreatorId,
                                    LikeCount = p.LikeCount,
                                    RetweetCount = p.RetweetCount,
                                    IsRetweet = p.IsRetweet,
                                }).ToListAsync();
            List<ResponseTweetDto> SortedList = selfTweets.OrderByDescending(o => o.CreatedAt).ToList();
            return SortedList;
        }
        public async Task<ResponseTweetDto> GetTweetByIdHandler(int id)
        {
            var tweet = await _dbContext.Tweets.Where(p => p.Id == id).Select(p =>
                  new ResponseTweetDto()
                  {
                      Id = p.Id,
                      Content = p.Content,
                      CreatedAt = p.CreatedAt,
                      CreatorId = p.CreatorId,
                      LikeCount = p.LikeCount,
                      RetweetCount = p.RetweetCount,
                      IsRetweet = p.IsRetweet,

                  }).SingleOrDefaultAsync();

            if (tweet is null)
                throw new TwitterApiException(400, "Invalid tweet id");

            return tweet;
        }
        public async Task<List<ResponseTweetDto>> GetTweetByContentHandler(GetByTextDto request)
        {

            var tweet = await _dbContext.Tweets.Where(p => p.Content.ToLower().Contains(request.Content.ToLower())).Select(p =>
                  new ResponseTweetDto()
                  {
                      Id = p.Id,
                      Content = p.Content,
                      CreatedAt = p.CreatedAt,
                      CreatorId = p.CreatorId,
                      LikeCount = p.LikeCount,
                      RetweetCount = p.RetweetCount,
                      IsRetweet = p.IsRetweet,

                  }).ToListAsync();
            List<ResponseTweetDto> SortedList = tweet.OrderByDescending(o => o.CreatedAt).ToList();
            return SortedList;
        }

        public async Task<List<ResponseTweetDto>> GetTweetByHashtagHandler(GetByTextDto request)
        {
            var hashtag = await _dbContext.Hashtags.Where(p => p.Content.ToLower().Contains(request.Content.ToLower())).Select(p =>
                  new ResponseHashtagDto()
                  {
                      Id = p.Id,
                      Content = p.Content,

                  }).ToListAsync();

            var list = new List<ResponseTweetDto>();

            if (hashtag != null && hashtag.Count > 0)
            {
                foreach (var tag in hashtag)
                {

                    var relations = await _dbContext.HashtagTweetRelations.Where(p => p.HashtagId == tag.Id).Select(p =>
                      new HashtagTweetRelationEntity()
                      {
                          Id = p.Id,
                          HashtagId = p.HashtagId,
                          TweetId = p.TweetId

                      }).ToListAsync();

                    if (relations != null && relations.Count > 0)
                    {
                        foreach (var relation in relations)
                        {

                            var tweet = await _dbContext.Tweets.Where(p => p.Id == relation.TweetId).Select(p =>
                              new ResponseTweetDto()
                              {
                                  Id = p.Id,
                                  Content = p.Content,
                                  CreatedAt = p.CreatedAt,
                                  CreatorId = p.CreatorId,
                                  LikeCount = p.LikeCount,
                                  RetweetCount = p.RetweetCount,
                                  IsRetweet = p.IsRetweet,

                              }).SingleOrDefaultAsync();

                            list.Add(tweet);
                        }
                    }
                }
            }

            List<ResponseTweetDto> SortedList = list.OrderByDescending(o => o.CreatedAt).ToList();
            return SortedList;
        }
    }
}

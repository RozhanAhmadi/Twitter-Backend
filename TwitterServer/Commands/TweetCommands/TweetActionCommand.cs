using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterServer.Commands.TweetCommands;
using TwitterServer.Data;
using TwitterServer.Exceptions;
using TwitterServer.Models.Dto.HashtagDto;
using TwitterServer.Models.Dto.TweetDto;
using TwitterServer.Models.Dto.UserDto;
using TwitterServer.Models.Entity;
using TwitterServer.Utilities;

namespace TwitterServer.Commands.TweetCommands
{
    public class TweetActionCommand : ITweetActionCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public TweetActionCommand(AppDbContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _iHttpContextAccessor = iHttpContextAccessor;
        }
        private async Task<HashtagEntity> searchHashtag(AddHashtagDto hashtag)
        {
            var tag = await _dbContext.Hashtags.Where(x => x.Content == hashtag.Content.ToLower()).Select(p =>
            new HashtagEntity()
            {
                Id = p.Id,
                Content = p.Content,
                HashtagTweetRelations = p.HashtagTweetRelations,
            }).SingleOrDefaultAsync();
            return tag;
        }
        public async Task AddTweetHandler(AddTweetDto request)
        {
            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            int creatorID = int.Parse(ClaimExtensions.GetUserId(user));

            var newTweet = new TweetEntity()
            {
                Content = request.Content,
                CreatedAt = DateTime.Now,
                CreatorId = creatorID,
                LikeCount = 0,
                RetweetCount = 0,
                IsRetweet = false,
            };

            await _dbContext.Tweets.AddAsync(newTweet);
            await _dbContext.SaveChangesAsync();

            if (request.HashTags != null && request.HashTags.Count > 0)
            {
                var list = new List<TweetHashtagRelationEntity>();
                foreach (var hashtag in request.HashTags)
                {
                    var tag = await searchHashtag(hashtag);
                    if(tag is null)
                    {
                        tag = new HashtagEntity();
                        tag.Content = hashtag.Content.ToLower();
                        await _dbContext.Hashtags.AddAsync(tag);
                        await _dbContext.SaveChangesAsync();
                    }

                    var relationht = new HashtagTweetRelationEntity();
                    relationht.HashtagId = tag.Id;
                    relationht.TweetId = newTweet.Id;
                    await _dbContext.HashtagTweetRelations.AddAsync(relationht);
                    await _dbContext.SaveChangesAsync();


                    list.Add(new TweetHashtagRelationEntity()
                    {
                        TweetId = newTweet.Id,
                        HashtagId = tag.Id,
                    });
                }
                await _dbContext.TweetHashtagRelations.AddRangeAsync(list);
            }
            await _dbContext.SaveChangesAsync();

            var relationtu = new UserTweetRelationEntity();
            relationtu.TweetId = newTweet.Id;
            relationtu.UserId = creatorID;
            await _dbContext.UserTweetRelations.AddRangeAsync(relationtu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task LikeTweetsHandler(int id)
        {
            var tweet = await _dbContext.Tweets.Where(p => p.Id == id).Select(p =>
                  new TweetEntity()
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

            tweet.LikeCount++;
             _dbContext.Tweets.Update(tweet);
            await _dbContext.SaveChangesAsync();

            ClaimsPrincipal user = _iHttpContextAccessor.HttpContext.User;
            int userID = int.Parse(ClaimExtensions.GetUserId(user));

            var relation = new LikeTweetUserRelationEntity();
            relation.LikerUserId = userID;
            relation.TweetId = id;

            await _dbContext.LikeTweetUserRelations.AddAsync(relation);
            await _dbContext.SaveChangesAsync();
            
        }

        public async Task<List<ResponseUserDto>> GetTweetLikersHandler(int id)
        {
            var relations = await _dbContext.LikeTweetUserRelations.Where(p => p.TweetId == id).Select(p =>
                   new LikeTweetUserRelationEntity()
                   {
                       Id = p.Id,
                       TweetId = p.TweetId,
                       LikerUserId = p.LikerUserId

                   }).ToListAsync();

            var list = new List<ResponseUserDto>();
            if (relations != null && relations.Count > 0)
            {
                foreach(var relation in relations)
                {
                    var user = await _dbContext.Users.Where(p => p.Id == relation.LikerUserId).Select(p =>
                        new ResponseUserDto()
                        {
                            Id = p.Id,
                            Username = p.Username,
                            Email = p.Email,
                            Picture = p.Picture,

                         }).SingleOrDefaultAsync();
                    list.Add(user);
                }
            }

            return list;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Commands.TweetCommands;
using TwitterServer.Models.Dto.TweetDto;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Controllers
{
    [Route("api/Tweet")]
    [ApiController]
    public class TweetController : ControllerBase
    {

        private readonly ITweetActionCommand _iTweetActionCommand;
        private readonly IGetTweetCommand _iGetTweetCommand;
        public TweetController(ITweetActionCommand iTweetActionCommand, IGetTweetCommand iGetTweetCommand)
        {

            _iTweetActionCommand = iTweetActionCommand;
            _iGetTweetCommand = iGetTweetCommand;
        }

        [Authorize]
        [HttpPost()]
        public async Task AddTweet(AddTweetDto request)
        {
            await _iTweetActionCommand.AddTweetHandler(request);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task DeleteTweet(int id)
        {
            await _iTweetActionCommand.DeleteTweetsHandler(id);
        }
        [Authorize]
        [HttpGet("like/{id}")]
        public async Task LikeTweet(int id)
        {
             await _iTweetActionCommand.LikeTweetsHandler(id);
        }
        
        [Authorize]
        [HttpGet("like/likers/{id}")]
        public async Task<List<ResponseUserDto>> GetTweetLikers(int id)
        {
           return await _iTweetActionCommand.GetTweetLikersHandler(id);
        }
        [Authorize]
        [HttpGet("retweet/{id}")]
        public async Task Retweet(int id)
        {
            await _iTweetActionCommand.RetweetHandler(id);
        }

        [Authorize]
        [HttpGet("HomeTweets")]
        public async Task<List<ResponseTweetDto>> GetHomeTweets()
        {
            return await _iGetTweetCommand.GetHomeTweetsHandler();
        }
        [Authorize]
        [HttpGet("SelfTweets")]
        public async Task<List<ResponseTweetDto>> GetSelfTweets()
        {
            return await _iGetTweetCommand.GetSelfTweetsHandler();
        }

        [HttpGet("{id}")]
        public async Task<ResponseTweetDto> GetTweetById(int id)
        {
            return await _iGetTweetCommand.GetTweetByIdHandler(id);
        }

        [HttpPost("SearchByContent")]
        public async Task<List<ResponseTweetDto>> GetTweetByContent(GetByTextDto request)
        {
            return await _iGetTweetCommand.GetTweetByContentHandler(request);
        }
        [HttpPost("SearchByHashtag")]
        public async Task<List<ResponseTweetDto>> GetTweetByHashtag(GetByTextDto request)
        {
            return await _iGetTweetCommand.GetTweetByHashtagHandler(request);
        }
    }
}

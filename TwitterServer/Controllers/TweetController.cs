using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Commands.TweetCommands;
using TwitterServer.Models.Dto.TweetDto;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.TweetDto;
using TwitterServer.Models.Dto.UserDto;

namespace TwitterServer.Commands.TweetCommands
{
    public interface ITweetActionCommand
    {
        public Task AddTweetHandler(AddTweetDto request);
        public Task LikeTweetsHandler(int id);
        public Task<List<ResponseUserDto>> GetTweetLikersHandler(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.TweetDto;

namespace TwitterServer.Commands.TweetCommands
{
    public interface IGetTweetCommand
    {
        public Task<List<ResponseTweetDto>> GetHomeTweetsHandler();
        public Task<List<ResponseTweetDto>> GetSelfTweetsHandler();
        public Task<List<ResponseTweetDto>> GetTweetByContentHandler(GetByTextDto request); 
        public Task<List<ResponseTweetDto>> GetTweetByHashtagHandler(GetByTextDto request); 
        public Task<ResponseTweetDto> GetTweetByIdHandler(int id);
    }
}

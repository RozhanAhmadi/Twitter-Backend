using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.TweetDto;

namespace TwitterServer.Commands.TweetCommands
{
    public interface ITweetActionCommand
    {
        public Task AddTweetHandler(AddTweetDto request);
    }
}

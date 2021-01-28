using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto;
using TwitterServer.Models.Dto.HashtagDto;

namespace TwitterServer.Commands
{
    public interface IInfoCommand
    {
        public Task<List<ResponseHashtagDto>> GetTopHashtagsHandler();
        public Task<List<ActivityLogDto>> GetSelfLogHandler();
        public Task<List<ActivityLogDto>> GetHomeLogHandler();
    }
}

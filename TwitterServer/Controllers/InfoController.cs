using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Commands;
using TwitterServer.Commands.InfoCommands;
using TwitterServer.Models.Dto;
using TwitterServer.Models.Dto.ActivityDto;
using TwitterServer.Models.Dto.HashtagDto;

namespace TwitterServer.Controllers
{
    [Route("api/Info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IInfoCommand _iInfoCommand;

        public InfoController(IInfoCommand iInfoCommand)
        {
            _iInfoCommand = iInfoCommand;
        }

        [HttpGet("TopHashtags")]
        public async Task<List<ResponseHashtagDto>> GetTopHashtags()
        {
            return await _iInfoCommand.GetTopHashtagsHandler();
        }

        [Authorize]
        [HttpGet("SelfLogs")]
        public async Task<List<ActivityLogDto>> GetSelfLog()
        {
            return await _iInfoCommand.GetSelfLogHandler();
        }

        [Authorize]
        [HttpGet("HomeLogs")]
        public async Task<List<ActivityLogDto>> GetHomeLog()
        {
            return await _iInfoCommand.GetHomeLogHandler();
        }
    }
}

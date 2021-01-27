using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TwitterServer.Models.Dto.HashtagDto;
using TwitterServer.Models.Entity;

namespace TwitterServer.Models.Dto.TweetDto
{
    public class AddTweetDto
    {
        [Required]
        public string Content { get; set; } = string.Empty;
        public List<AddHashtagDto> HashTags { get; set; }
    }
}

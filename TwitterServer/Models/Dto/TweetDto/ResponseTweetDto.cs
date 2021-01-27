using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Dto.TweetDto
{
    public class ResponseTweetDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }
        public int LikeCount { get; set; }
        public int RetweetCount { get; set; }
        public bool IsRetweet { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class UserTweetRelationEntity
    {
        public UserTweetRelationEntity()
        {
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TweetId { get; set; }
    }
}

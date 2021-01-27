using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class LikeTweetUserRelationEntity
    {
        public LikeTweetUserRelationEntity()
        {
        }
        public int Id { get; set; }
        public int TweetId { get; set; }
        public int LikerUserId { get; set; }
    }
}


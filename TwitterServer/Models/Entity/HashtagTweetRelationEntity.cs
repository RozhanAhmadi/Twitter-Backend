using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class HashtagTweetRelationEntity
    {
        public HashtagTweetRelationEntity()
        {
        }
        public int Id { get; set; }
        public int HashtagId { get; set; }
        public int TweetId { get; set; }
    }
}

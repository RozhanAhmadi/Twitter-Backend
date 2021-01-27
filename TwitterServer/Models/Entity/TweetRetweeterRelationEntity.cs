using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class TweetRetweeterRelationEntity
    {
        public TweetRetweeterRelationEntity()
        {
        }
        public int Id { get; set; }
        public int TweetId { get; set; }
        public int RetweeterId { get; set; }
    }
}

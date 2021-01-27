using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class TweetEntity
    {
        public TweetEntity()
        {
            TweetRetweeterRelations = new HashSet<TweetRetweeterRelationEntity>();
            TweetHashtagRelations = new HashSet<TweetHashtagRelationEntity>();
        }
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }
        public int LikeCount { get; set; }
        public int RetweetCount { get; set; }
        public bool IsRetweet { get; set; }
        public ICollection<TweetRetweeterRelationEntity> TweetRetweeterRelations { get; set; }
        public ICollection<TweetHashtagRelationEntity> TweetHashtagRelations { get; set; }

    }
}

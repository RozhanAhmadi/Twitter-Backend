using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class HashtagEntity
    {
        public HashtagEntity()
        {
            HashtagTweetRelations = new HashSet<HashtagTweetRelationEntity>();
        }
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public int UsageCount { get; set; }
        public ICollection<HashtagTweetRelationEntity> HashtagTweetRelations { get; set; }
    }
}

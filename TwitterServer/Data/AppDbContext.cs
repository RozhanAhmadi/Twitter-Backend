using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwitterServer.Models.Entity;

namespace TwitterServer.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TweetEntity> Tweets { get; set; }
        public DbSet<HashtagEntity> Hashtags { get; set; }
        public DbSet<UserFollowRelationEntity> UserFollowRelations { get; set; }
        public DbSet<TweetRetweeterRelationEntity> TweetRetweeterRelations { get; set; }
        public DbSet<TweetHashtagRelationEntity> TweetHashtagRelations { get; set; }
        public DbSet<HashtagTweetRelationEntity> HashtagTweetRelations { get; set; }
        public DbSet<UserTweetRelationEntity> UserTweetRelations { get; set; }

    }
}

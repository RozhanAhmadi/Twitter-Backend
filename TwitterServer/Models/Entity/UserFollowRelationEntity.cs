using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public partial class UserFollowRelationEntity
    {
        public UserFollowRelationEntity()
        {
        }
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        // follower follows following
    }
}

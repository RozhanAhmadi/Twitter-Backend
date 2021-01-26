using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Entity
{
    public class UserEntity
    {
       public UserEntity()
        {
            UserFollowRelations = new HashSet<UserFollowRelationEntity>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserFollowRelationEntity> UserFollowRelations { get; set; }
    }
}

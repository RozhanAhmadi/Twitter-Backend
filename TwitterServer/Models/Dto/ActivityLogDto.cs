using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Dto
{
    public class ActivityLogDto
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public string ActionTypeName { get; set; }
        public int ActionTypeId { get; set; }
        public int TargetTweetId { get; set; }
        public int TargetUserId { get; set; }
        public DateTime Date { get; set; }
    }
}

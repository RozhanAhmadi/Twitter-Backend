using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterServer.Models.Dto.UserDto
{
    public class AddUserDto
    {
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress()]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    
}

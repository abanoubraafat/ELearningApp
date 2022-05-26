using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string ProfilePic { get; set; }
        public string Role { get; set; }
    }
}

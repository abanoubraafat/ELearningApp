using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LoginResponse
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        //public string Username { get; set; }
        public int? Id { get; set; }
        [EmailAddress]
        public string? EmailAddress { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public string? ProfilePic { get; set; }
        //public string? ProfilePic { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }
        //[JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }

    }
}

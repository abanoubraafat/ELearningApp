using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LoginRequest
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}

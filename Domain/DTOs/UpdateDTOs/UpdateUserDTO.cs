using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UpdateDTOs
{
    public class UpdateUserDTO: UserDTO
    {
        public new string? ProfilePic { get; set; }
        public new string? Password { get; set; }

    }
}

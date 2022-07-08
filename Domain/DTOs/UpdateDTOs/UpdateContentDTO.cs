using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UpdateDTOs
{
    public class UpdateContentDTO : ContentDTO
    {
        public new string? Path { get; set; }
    }
}

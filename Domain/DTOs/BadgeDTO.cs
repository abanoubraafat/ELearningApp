using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class BadgeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int AssignmentAnswerId { get; set; }
    }
}

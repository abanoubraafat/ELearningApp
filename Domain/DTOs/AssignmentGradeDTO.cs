using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentGradeDTO
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public int AssignmentAnswerId { get; set; }
    }
}

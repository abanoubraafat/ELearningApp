using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Submitted { get; set; }
        public int? AssignedGrade { get; set; }
        
    }
}

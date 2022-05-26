using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }  
        public DateTime? StartDate { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Grade { get; set; }
        public int CourseId { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
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
        public IFormFile? FilePath { get; set; }  
        public DateTime? StartDate { get; set; }
        public DateTime? EndTime { get; set; }
        public int TotalPoints { get; set; }
        public int CourseId { get; set; }
    }
}

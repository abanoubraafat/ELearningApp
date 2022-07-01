using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public IFormFile? CourseImage { get; set; }
        public int TeacherId { get; set; }

    }
}

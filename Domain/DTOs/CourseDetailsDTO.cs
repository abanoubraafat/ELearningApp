using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CourseDetailsDTO : CourseDTO
    {
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public new string? CourseImage { get; set; }
    }
}

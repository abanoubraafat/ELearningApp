using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CourseDetailsDTO : CourseDTO
    {
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherEmailAddress { get; set; }
        public string TeacherPhone { get; set; }
        public string? TeacherProfilePic { get; set; }
        public new string? CourseImage { get; set; }
    }
}

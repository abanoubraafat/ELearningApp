using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CourseStudentUnVerifiedRequestsDTO
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string? StudentProfilePic { get; set; }
        public string CourseName { get; set; }
        public string? CourseImage { get; set; }
    }
}

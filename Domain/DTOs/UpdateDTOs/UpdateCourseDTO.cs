using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UpdateDTOs
{
    public class UpdateCourseDTO :CourseDTO
    {
        public new string? CourseImage { get; set; }
    }
}

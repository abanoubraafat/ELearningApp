using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentAnswerGradeShortDTO
    {
        public int AssignmentId { get; set; }
        public string AssignmentTitle { get; set; }
        public int AssignmentTotalPoints { get; set; }
        public int? AssignedGrade { get; set; }
    }
}

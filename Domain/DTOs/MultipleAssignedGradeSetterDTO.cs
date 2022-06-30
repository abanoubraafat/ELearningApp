using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class MultipleAssignedGradeSetterDTO
    {
        public int[] Ids { get; set; }
        public int[] AssignedGrades { get; set; }
    }
}

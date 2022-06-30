using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UpdateDTOs
{
    public class UpdateAssignmentAnswerDTO : AssignmentAnswerDTO
    {
        public new string PDF { get; set; }

    }
}

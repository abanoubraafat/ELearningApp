using ELearning_App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.GetDTOs
{
    public class GetAssignmentDTO : AssignmentDTO
    {
        public new string? FilePath { get; set; }
        [JsonIgnore]
        public virtual ICollection<AssignmentAnswer> AssignmentAnswers { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.GetDTOs
{
    public class GetAssignmentWithSubmitted : AssignmentDTO
    {
        public new string? FilePath { get; set; }
        public bool Submitted { get; set; }
        [JsonIgnore]
        public int AssignmentAnswerId { get; set; }
    }
}

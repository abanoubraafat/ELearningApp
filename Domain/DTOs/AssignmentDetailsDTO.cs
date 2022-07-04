using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Submitted { get; set; }
        public int TotalPoints { get; set; }
        public string? FilePath { get; set; }
        public string? Description { get; set; }
        public int? AssignedGrade { get; set; }
        [JsonIgnore]
        public int AssignmentAnswerId { get; set; }
        
    }
}

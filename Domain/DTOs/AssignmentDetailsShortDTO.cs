using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentDetailsShortDTO
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public int TotalPoints { get; set; }
        public int? AssignedGrade { get; set; }
        public bool Submitted { get; set; }
        [JsonIgnore]
        public int AssignmentAnswerId { get; set; }

    }
}

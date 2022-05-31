using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentAnswerDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string PDF { get; set; }
        public DateTime SubmitDate { get; set; }
        //public DateTime SubmitTime { get; set; } 
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
    }
}

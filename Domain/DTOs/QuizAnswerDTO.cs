using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuizAnswerDTO
    {
        public int Id { get; set; }
        public string Answer { get; set; } 
        public DateTime SubmitTime { get; set; }
        public int QuizId { get; set; }
        public int StudentId { get; set; }
    }
}

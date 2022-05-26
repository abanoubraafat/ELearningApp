using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuestionAnswerDTO
    {
        public int Id { get; set; }
        public string QuestionAnswerText { get; set; }
        public bool State { get; set; }
        public int QuestionId { get; set; }
        public int StudentId { get; set; }
    }
}

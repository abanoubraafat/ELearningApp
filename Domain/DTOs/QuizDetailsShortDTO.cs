using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuizDetailsShortDTO
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public int TotalPoints { get; set; }
        public int? AssignedGrade { get; set; }
    }
}

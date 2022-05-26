using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string Firstchoise { get; set; } 
        public string Secondchoise { get; set; } 
        public string Lastchoise { get; set; } 
        public string CorrectAnswer { get; set; }
        public DateTime ShowDate { get; set; } 
        public int LessonId { get; set; }
    }
}

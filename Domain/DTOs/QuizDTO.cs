using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string QuizFile { get; set; } 
        public string modelAnswer { get; set; }
        public int Grade { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseId { get; set; }
    }
}

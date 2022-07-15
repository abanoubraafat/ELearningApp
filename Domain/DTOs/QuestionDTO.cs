using Domain.Entities;
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
        public string Title { get; set; }
        public string correctAnswer { get; set; }
        public DateTime? ShowDate { get; set; }
        public int QuizId { get; set; }
        public ICollection<QuestionChoiceDTO>? QuestionChoices { get; set; }

    }
}

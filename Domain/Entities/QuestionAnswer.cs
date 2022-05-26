using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string QuestionAnswerText { get; set; } // chosen answer from mcq
        public bool State { get; set; } // true or false
        //Relationships:

        // one to many (Question-> QuestionAnswer)
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        // one to many (Student -> QuestionAnswer)
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        //public int ChoiceId { get; set; }
        //public virtual Choice Choice { get; set; }

    }
}

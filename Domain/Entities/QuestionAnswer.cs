using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; } // chosen answer from mcq
        //public bool State { get; set; } // true or false
        //Relationships:

        // one to many (Question-> QuestionAnswer)
        public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }

        // one to many (Student -> QuestionAnswer)
        public int StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }
        //public int QuizId { get; set; }
        //public virtual Quiz Quiz { get; set; }
        //public int ChoiceId { get; set; }
        //public virtual Choice Choice { get; set; }

    }
}

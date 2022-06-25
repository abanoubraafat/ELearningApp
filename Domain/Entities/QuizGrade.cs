using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class QuizGrade
    {
        public int Id { get; set; }
        public int Grade { get; set; }

        //Relationships:

        //(QuizAnswers --> QuizGrade) --> one to one.
        //public int QuizAnswerId { get; set; }
        //public virtual QuizAnswer QuizAnswer { get; set; }
        public int QuizId { get; set; }
        [JsonIgnore]
        public virtual Quiz Quiz { get; set; }
        public int StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }
        
        //json: quiz name, student name, grade
    }
}

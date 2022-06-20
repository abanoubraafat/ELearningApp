using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Quiz Quiz { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        
        //json: quiz name, student name, grade
    }
}

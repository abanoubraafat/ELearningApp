//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ELearning_App.Domain.Entities
//{
//    public class QuizAnswer
//    {
//        public int Id { get; set; }
//        public string Answer { get; set; } //as a path 
//        public DateTime SubmitTime { get; set; }


//        //Relationships:

//        // (Quiz --> QuizAnswer) one to many
//        public int QuizId { get; set; }
//        public virtual Quiz Quiz { get; set; }

//        // (Student --> QuizAnswer) one to many
//        public int StudentId { get; set; }
//        public virtual Student Student { get; set; }

//        //(QuizAnswer --> QuizGrade) --> one to one.
//        //[ForeignKey("QuizGrade")]
//        //public int QuizGradeId { get; set; }
//        //public virtual QuizGrade QuizGrade { get; set; }


//    }
//}

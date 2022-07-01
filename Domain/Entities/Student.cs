using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    // child
    [Table("Students")]

    public class Student : User
    {
        // Relationships:
        // one to one  -->   ( parent -> student) 
        //public virtual Parent Parent { get; set; }
        // Relationships:

        //many to many  --> student -> Courses
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        [JsonIgnore]
        public virtual ICollection<Parent> Parents { get; set; } = new HashSet<Parent>();


        // one to many (Student -> QuestionAnswer)
        //public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new HashSet<QuestionAnswer>();

        // one to many (Student -> LatestPassedLesson)
        //public virtual ICollection<LatestPassedLesson> LatestPassedLessons { get; set; } = new HashSet<LatestPassedLesson>();

        // one to many (Student -> Notes)
        //public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note> ();

        // (Student --> AssignmentAnswer) --> one to many
        //public virtual ICollection<AssignmentAnswer> AssignmentAnswers { get; set; } = new HashSet <AssignmentAnswer>();


        // (Student --> QuizAnswer) one to many
        //public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }= new HashSet<QuizAnswer>();


        // (Student -> Feature ) one to many 
        //public virtual ICollection<Feature> Features {get; set;} = new HashSet<Feature> (); 
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        // not nesseary 
        public string? CourseDescription { get; set; }
        // not nesseary 
        public string? CourseImage { get; set; }    // as link

        // start & end date of course

        // RelationShips:

        // many to many  course --> student
        //[JsonIgnore]
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        // one to many  (teacher -> courses)
        public int TeacherId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Teacher Teacher { get; set; }

        // one to many (course -> lesson)
        //public virtual ICollection<Lesson> Lessons { get; set; }= new HashSet<Lesson>();

        // one to many  (Course -> LatestPassedLesson)
        //public virtual ICollection<LatestPassedLesson> LatestPassedLessons { get;set; }= new HashSet<LatestPassedLesson>();

        // one to many  (Course --> Assignments)
        //public virtual ICollection<Assignment> Assignments { get; set; } = new HashSet<Assignment>();

        // one to many  (Course --> Quiz)
        //public virtual ICollection<Quiz> Quizzes { get; set; } = new HashSet<Quiz>();

        // many to many (Course -> Announcement)
        //public virtual ICollection<Announcement> Announcements { get; set; } = new HashSet<Announcement>();


       
    }
}

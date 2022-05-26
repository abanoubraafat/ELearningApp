using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class LatestPassedLesson
    {
        public int Id { get; set; }
        public string LatestLessonName { get; set; }

        // Relationships:
        
        // one to many  (Course -> LatestPassedLesson)
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        // one to many (Student -> LatestPassedLesson)
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}

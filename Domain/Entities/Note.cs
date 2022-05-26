using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }

        public string NoteText { get; set; }

        // Relationships:
        //one to many (Lesson -> Notes)
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        // one to many (Student -> Notes)
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}

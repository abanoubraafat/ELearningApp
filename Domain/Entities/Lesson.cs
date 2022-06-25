using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // Relationships:

        // one to many (Course --> lesson)
        public int CourseId { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }

        // one to many  (Lesson --> Content)
        //public virtual ICollection<Content> Contents { get; set; } = new HashSet<Content>();

        //// one to many (lesson -> Questions)
        //public virtual ICollection<Question> Questions { get; set; }= new HashSet<Question>();

        ////one to many (Lesson -> Notes)
        //public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();
    }
}

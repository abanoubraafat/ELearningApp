using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Content
    {
        [Key]
        public long Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; } // stored as string not byte[]
                                         // not nesseary 
        public DateTime ShowDate { get; set; } // available on ..

        //Relationships

        // (Lesson --> course_content_file) one to many
        public int LessonId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Lesson Lesson { get; set; }
        //public int SectionId { get; set; }
        //public virtual Section Section { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Content
    {
        
        public int Id { get; set; }
        //public string FileName { get; set; }
        public string? VideoPath { get; set; }
        public string? PdfPath { get; set; }
        public string? Text { get; set; }
        public string? Link { get; set; }
        public DateTime ShowDate { get; set; } // available on ..
        //string content
        //Relationships

        // (Lesson --> course_content_file) one to many
        public int LessonId { get; set; }
        [JsonIgnore]
        public virtual Lesson Lesson { get; set; }
        //public int SectionId { get; set; }
        //public virtual Section Section { get; set; }


    }
}

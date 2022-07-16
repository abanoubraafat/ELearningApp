using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        // not nesseary 
        public string? Instructions { get; set; }
        //public string QuizFile { get; set; } // as a link (PDF Questions)
        //public string modelAnswer { get; set; } // as a link (PDF Solutions)
        public int TotalPoints { get; set; }

        //public DateTime Duration { get; set; }  // ex: 02:30:00  
        public DateTime? PostTime { get; set; } // posting date
        public DateTime StartTime { get; set; } // ex: 01:00:00
        public DateTime EndTime { get; set; } // ex: 03:30:00

        //Relationships:

        // (Course --> Quiz) one to many
        public int CourseId { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }

        // (Quiz --> QuizAnswer) one to many
        //public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new HashSet<QuizAnswer>();
        public ICollection<QuizGrade> QuizGrades { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}

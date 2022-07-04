using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        // not nesseary 
        public string? FilePath { get; set; }  // if the assignment is a PDF file 
        public DateTime? StartDate { get; set;} 
        // not nesseary 
        public DateTime? EndTime { get; set;}
        // -/10 for example from 10
        public int TotalPoints { get; set; }
        // not nesseary 
        //public DateTime ShowDate { get; set; }

        //Relationships:

        // (course --> Assignment) one to many
        public int CourseId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Course Course { get; set; }
        public virtual ICollection<AssignmentAnswer> AssignmentAnswers { get; set; }

        // (Assignment --> AssignmentAnswer) --> one to many 
        //public virtual ICollection<AssignmentAnswer> AssignmentAnswers { get; set; } = new HashSet<AssignmentAnswer>();

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class AssignmentAnswer
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        public string PDF { get; set; } //as link URL
        public DateTime SubmitDate { get; set; }

        //public DateTime SubmitTime { get; set; }
        //Relationships:

        // (Assignment --> AssignmentAnswer) --> one to many 
        public int AssignmentId { get; set; }
        [JsonIgnore]
        public virtual Assignment Assignment { get; set; }

        // (Student --> AssignmentAnswer) --> one to many
        public int StudentId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set;}


        //(AssignmentAnswers --> AssignmentGrade) --> one to one.
        //[ForeignKey("AssignmentGrade")]
        //public int AssignmentGradeId { get; set; }
        //public virtual AssignmentGrade AssignmentGrade { get; set; }

        // one to one (AssignmentAnswer -> AssignmentFeedback)
        //[ForeignKey("AssignmentFeedback")]
        //public int AssignmentFeedbackId { get; set; }
        //public virtual AssignmentFeedback AssignmentFeedback { get; set; }

        // one to one (Badge -> AssignmentAnswer)
        //[ForeignKey("Badge")]
        //public int BadgeId { get; set; }
        //public virtual Badge Badge { get; set; }

    }
}

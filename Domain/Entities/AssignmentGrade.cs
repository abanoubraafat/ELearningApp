using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class AssignmentGrade
    {
        public int Id { get; set; }
        public int Grade { get; set; }

        //Relationships:
        
        //(AssignmentAnswers --> AssignmentGrade) --> one to one.
        public int AssignmentAnswerId { get; set; }
        [JsonIgnore]
        public virtual AssignmentAnswer AssignmentAnswer { get; set; }
    }
}

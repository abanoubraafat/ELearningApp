using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    public class Badge
    {
        public int Id { get; set; }
        // not nesseary 
        public string Title { get; set; }
        public string Image { get; set; } // as a path

        // Relationships:

        // one to one (Badge -> AssignmentAnswer)
        public int AssignmentAnswerId { get; set; }
        [JsonIgnore]
        public virtual AssignmentAnswer AssignmentAnswer { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    //child
    [Table("Parents")]
    public class Parent: User
    {
        //Relationships :
        //one to one -->  parent --> student
        //public int StudentId { get; set; }
        //public virtual Student Student { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        //many student one parent
        //also service

    }
}

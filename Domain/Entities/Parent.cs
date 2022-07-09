using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    //child
    [Table("Parents")]
    public class Parent: User
    {

        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        [JsonIgnore]
        public List<ParentStudent> ParentStudents { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    // child
    [Table("Teachers")]
    public class Teacher : User
    {
        // Relationships:
        // one to many  teacher --> courses
        //public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<Student> Students { get; set; }

        //many student one parent
        //also service

    }
}

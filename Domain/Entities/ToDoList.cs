using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        // not nesseary 
        public DateTime Date { get; set; }
        // not nesseary 
        //public DateTime Time { get; set; }
        // not nesseary 
        public bool Done { get; set; }
        // not nesseary 
        public bool? Urgent { get; set; }
        // not nesseary 
        public bool? Important { get; set; }
        //Relationship:
        
        // (loginInfo(users) --> ToDoList) one to many  , users ->(Student & Teacher)
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string OldPath { get; set; } // (image , file , video , audio)
        public string NewPath { get; set; } // or output from moodels as string 

        // Relationships:
        //one to many (Student -> Feature)
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}

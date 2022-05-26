using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        // not nesseary 
        public string Image { get; set; }
        public string Type { get; set; } // (website , channel , book , article , others)
    }
}

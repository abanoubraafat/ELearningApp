using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        public string AnnouncementContent { get; set; } // text or path of a file
        public DateTime PostDate { get; set; }

        // many to many (Announcement-> Course)
        //public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}

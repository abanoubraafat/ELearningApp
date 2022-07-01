using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ELearning_App.Domain.Entities
{
    // Parent Class
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        // not nesseary 
        public string? ProfilePic { get; set; }
        //public string? ProfilePic { get; set; }

        // type indicates if the user is student or teacher or parent
        // can be stored in Enum as int  student -> 1 , teacher -> 2 , parent -> 3
        public string Role { get; set; }

        // Relationships:
        // (loginInfo(users) --> ToDoList) one to many  , users ->(Student & Teacher)
        //public virtual ICollection<ToDoList> ToDoLists { get; set; } = new HashSet<ToDoList> ();
        [JsonIgnore]
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}

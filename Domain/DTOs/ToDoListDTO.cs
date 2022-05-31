using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } 
        public bool Done { get; set; }
        public bool? Urgent { get; set; }
        public bool? Important { get; set; }
        public int UserId { get; set; }
    }
}

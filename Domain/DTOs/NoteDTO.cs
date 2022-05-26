using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set; }
    }
}

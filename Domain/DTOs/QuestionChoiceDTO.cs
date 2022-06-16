using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuestionChoiceDTO
    {
        public int Id { get; set; }
        public string Choice { get; set; }
        public int QuestionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuizGradeDetailsDTO : QuizGradeDTO
    {
        public string QuizTitle { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        
    }
}

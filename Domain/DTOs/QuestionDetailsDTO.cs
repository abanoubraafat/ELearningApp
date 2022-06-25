using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuestionDetailsDTO : QuestionDTO
    {
        public virtual IEnumerable<QuestionChoice> QuestionChoices { get; set; }

    }
}

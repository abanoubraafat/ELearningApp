using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuestionDetailsDTO : QuestionDTO
    {
        [JsonIgnore]
        public virtual IEnumerable<QuestionChoice> QuestionChoices { get; set; }

    }
}

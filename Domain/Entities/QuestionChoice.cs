using ELearning_App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuestionChoice
    {
        public int Id { get; set; }
        public string Choice { get; set; }
        //public bool Correct { get; set; }
        public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }

    }
}

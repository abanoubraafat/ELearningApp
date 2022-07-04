using ELearning_App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs.GetDTOs
{
    public class GetQuizDTO : QuizDTO
    {
        [JsonIgnore]
        public ICollection<QuizGrade> QuizGrades { get; set; }
    }
}

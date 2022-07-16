using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.GetDTOs
{
    public class GetQuizWithAllInfoDTO :GetQuizDTO
    {
        public ICollection<QuestionDTO>? Questions { get; set; }

    }
}

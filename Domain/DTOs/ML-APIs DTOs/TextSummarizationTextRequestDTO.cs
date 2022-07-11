using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs
{
    public class TextSummarizationTextRequestDTO
    {
        public string Text { get; set; }
        public string Sentnum { get; set; }
    }
}

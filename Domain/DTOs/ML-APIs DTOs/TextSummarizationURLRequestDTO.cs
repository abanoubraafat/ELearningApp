using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs
{
    public class TextSummarizationURLRequestDTO
    {
        public string Url { get; set; }
        public string Sentnum { get; set; }
    }
}

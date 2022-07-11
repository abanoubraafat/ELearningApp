using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs
{
    public class TextSimilarityRequestDTO
    {
        public string text1 { get; set; }
        public string text2 { get; set; }
    }
}

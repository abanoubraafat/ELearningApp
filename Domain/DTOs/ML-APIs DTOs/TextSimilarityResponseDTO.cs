using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs
{
    public class TextSimilarityResponseDTO
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public int Result_code { get; set; }
        public string Result_msg { get; set; }
        public double Similarity { get; set; }
        public double Value { get; set; }
        public string Version { get; set; }
    }
}

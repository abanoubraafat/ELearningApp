using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs
{
    public class TranslateRequestDTO
    {
        public string TextToBeTranslated { get; set; }
        public string TargetLang { get; set; }
        public string? SourceLang { get; set; }

    }
}

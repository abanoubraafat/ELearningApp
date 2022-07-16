using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs.GoogleTranslate
{
    public class Translation
    {
        public string TranslatedText { get; set; }
        public string? DetectedSourceLanguage { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ML_APIs_DTOs
{
    public class TableExtractionRequestDTO
    {
        public IFormFile file { get; set; }
    }
}

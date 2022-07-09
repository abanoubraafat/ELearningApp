using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UpdateDTOs
{
    public class UpdateVidPdfContentDTO
    {
        public IFormFile? PdfPath { get; set; }
        public IFormFile? VideoPath { get; set; } 
    }
}

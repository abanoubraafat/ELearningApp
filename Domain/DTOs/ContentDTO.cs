﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ContentDTO
    {
        public int Id { get; set; }
        //public string FileName { get; set; }
        public IFormFile? VideoPath { get; set; }
        public IFormFile? PdfPath { get; set; }
        public string? Text { get; set; }
        public string? Link { get; set; }
        public DateTime ShowDate { get; set; } 
        public int LessonId { get; set; }
    }
}

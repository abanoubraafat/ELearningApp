﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuizGradeDTO
    {
        public int Id { get; set; }
        public int? AssignedGrade { get; set; }
        public int QuizId { get; set; }
        public int StudentId { get; set; }
    }
}

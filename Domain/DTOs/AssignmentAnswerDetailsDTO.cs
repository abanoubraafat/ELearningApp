﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AssignmentAnswerDetailsDTO : AssignmentAnswerDTO
    {
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public new string PDF { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string OldPath { get; set; } 
        public string NewPath { get; set; }  
        public int StudentId { get; set; }
    }
}

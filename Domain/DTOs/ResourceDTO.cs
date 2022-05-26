using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
    }
}

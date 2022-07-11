using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PostMultipleParentStudentReqDTO
    {
        public int ParentId { get; set; }
        public List<string> StudentsEmails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consts
{
    public static class FilesConstraints
    {
        public static readonly List<string> allowedExtenstions = new List<string> 
        { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xlsx", ".rar", ".zip", ".png", ".jpg", ".jpeg", ".txt", ".html", ".htm" };
        public const long maxAllowedSize = 5242880;
    }
}

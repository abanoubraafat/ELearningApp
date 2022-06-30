using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consts
{
    public static class ContentConstraints
    {
        public static readonly List<string> allowedExtenstions = new List<string>
        { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xlsx", ".rar", ".zip", ".png", ".jpg", ".jpeg", ".txt",
        ".mp4", ".mp3", ".mkv"};
    }
}

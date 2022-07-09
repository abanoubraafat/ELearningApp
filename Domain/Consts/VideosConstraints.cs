using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consts
{
    public class VideosConstraints
    {
        public static readonly List<string> allowedExtenstions = new List<string>
        {".mp4", ".mkv"};
    }
}

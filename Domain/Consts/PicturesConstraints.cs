using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consts
{
    public static class PicturesConstraints
    {
        public static readonly List<string> allowedExtenstions = new List<string> { ".jpg", ".png", ".jpeg" };
        public const long maxAllowedSize = 5242880;
    }
}

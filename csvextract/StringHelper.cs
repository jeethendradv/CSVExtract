using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvextract
{
    public static class StringHelper
    {
        public static string TrimDoubleQuotes(this string h)
        {
            while (true)
            {
                if (h[0].ToString() != "\"") break;
                h = h.Substring(1);
            }
            while (true)
            {
                if (h[h.Length - 1].ToString() != "\"") break;
                h = h.Substring(0, h.Length - 1);
            }
            return h;
        }

        public static string ReplaceInvalidFileNameChars(this string filename)
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }
            return filename;
        }
    }
}

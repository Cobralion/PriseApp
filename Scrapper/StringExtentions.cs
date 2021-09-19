using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper
{
    public static class StringExtentions
    {
        public static string CRLFToLF(this string source)
        {
            return source.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGen.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            var chars = str.ToCharArray();
            chars[0] = char.ToLower(chars[0]);

            return new string(chars);
        }
    }
}

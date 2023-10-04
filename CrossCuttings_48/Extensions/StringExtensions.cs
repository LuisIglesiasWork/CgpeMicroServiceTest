using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{
    public static class StringExtensions
    {
        public static String GetLimitedLengthString(this String str, int maxChars)
        {
            if(maxChars < 0)
            {
                maxChars = 0;
            }

            if (!String.IsNullOrWhiteSpace(str) && str.Length > 0)
            {
                return str.Length <= maxChars ? str : str.Length >= 4 ? str.Substring(0, maxChars - 3) + "..." : str.Substring(0, maxChars);
            }
            else
            {
                return str;
            }
        }
    }
}

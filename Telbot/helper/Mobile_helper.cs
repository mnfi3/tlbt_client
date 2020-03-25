using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Telbot.helper
{
    class Mobile_helper
    {
        public static string fixNumber(string number)
        {
            number = Regex.Replace(number, "[^0-9.]", "");

            if (number[0] == '0')
                number = number.Substring(1, number.Length - 1);
            if (number[0] == '0')
                number = number.Substring(1, number.Length - 1);
            if (number[0] == '0')
                number = number.Substring(1, number.Length - 1);

            if (number.Length == 10)
                number = "98" + number;

            return number;
        }
    }
}

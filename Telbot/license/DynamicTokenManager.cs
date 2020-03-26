using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.license
{
    class DynamicTokenManager
    {
        public  bool checkTokenValidity(string time_token)
        {
            string text = Crypt.DecryptString_256(time_token);
            if (text == "#") return false;
            int time = int.Parse(text);
            if (time > getTimeStamp2() || time < getTimeStamp1()) return false;

            return true;
        }

        private int getTimeStamp1()
        {
            int time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds  - 18000;
            return time;
        }

        private int getTimeStamp2()
        {
            int time = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds + 18000;
            return time;
        }
    }
}

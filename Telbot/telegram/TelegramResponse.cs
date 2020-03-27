using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.telegram
{
    public class TelegramResponse
    {
        public int status { set; get; }
        public string message { set; get; }
        public object data { set; get; }
    }
}

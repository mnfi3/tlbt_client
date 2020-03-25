using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.api
{
    class Response
    {
        public int status { set; get; }
        public string message { set; get; }
        public JObject data { set; get; }
        public string full_response { set; get; }
    }
}

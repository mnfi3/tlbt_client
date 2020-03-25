using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.model
{
    public class Mobile_model
    {
        public Int32 id { get; set; }
        public string number { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }


        public Mobile_model()
        {
            id = 0;
            number = "";
            first_name = "";
            last_name = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.model
{
    public class Setting_model
    {

        public int is_first_open { set; get; }
        public int delay_time { set; get; }

        public Setting_model()
        {
            is_first_open = 1;
            delay_time = 0;
        }


    }
}

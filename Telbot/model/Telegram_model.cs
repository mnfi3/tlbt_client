using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.model
{
    public class Telegram_model
    {


        public int is_logged_in { set; get; }
      


        public string api_hash { set; get; }
        public int api_id { set; get; }
        public string hash { set; get; }
        public List<string> mobiles { set; get; }
        public string mobile { set; get; }
        public string password { set; get; }
        public string user_id { set; get; }
        public string user_name { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }
        public string image { set; get; }
        public string bio { set; get; }


        public int getDelayTime()
        {
            int mobiles_count = this.mobiles.Count;
            int time = 30 / mobiles_count;

            if (time < 3) 
                return 3000;

            return time * 1000; //milliseconds
        }

        public int getAddTime(int contacts_count)
        {
            return ((this.getDelayTime() * contacts_count) / 1000) / 60;//minutes
        }

        public Telegram_model()
        {
            is_logged_in = 0;
            api_hash = "";
            api_id = 0;
            hash = "";
            mobile = "";
            mobiles = new List<string>();
            password = "";
            user_id = "";
            user_name = "";
            first_name = "";
            last_name = "";
            image = "";
            bio = "";
        }
    }
}

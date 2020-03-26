using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.model
{
    public class Telegram_model
    {


        public int is_logged_in { set; get; }
        public string api_hash { set; get; }
        public string api_id { set; get; }
        public string phone_number { set; get; }
        public string password { set; get; }
        public string user_id { set; get; }
        public string user_name { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }
        public string image { set; get; }
        public string bio { set; get; }

        public Telegram_model()
        {
            is_logged_in = 0;
            api_hash = "";
            api_id = "";
            phone_number = "";
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

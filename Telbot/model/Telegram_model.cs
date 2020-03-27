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
        public int is_session_exist
        {
            get
            {
                if (File.Exists("session.bat"))
                    return 1;
                return 0;
            }
        }


        public string api_hash { set; get; }
        public int api_id { set; get; }
        public string hash { set; get; }
        public string mobile { set; get; }
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
            api_id = 0;
            hash = "";
            mobile = "";
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

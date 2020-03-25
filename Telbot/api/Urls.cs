using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.api
{
    class Urls
    {

        public const string BASE_URL = "http://localhost:8080/telbot/api";
        public const string UPDATE_INFO_URL = "http://localhost:8080/telbot/download/updates.txt";

        //public const string BASE_URL = "https://easybazi.ir/public/kiosk/public/api";
        //public const string UPDATE_INFO_URL = "https://easybazi.ir/public/kiosk/public/download/updates.txt";


        public const string APP_LOGIN = BASE_URL + "/v1/member-app/login";
        public const string APP_AUTH_CHECK = BASE_URL + "/v1/member-app/auth-check";
    }
}

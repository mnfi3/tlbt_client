using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telbot.db;
using Telbot.license;
using Telbot.model;
using Telbot.storage;

namespace Telbot.system
{
    class G
    {
        public static string client_key;
        public static App_model app;
        public static bool isLoggedIn = false;
        public const string PUBLIC_KEY = "kkkF19BEE2EF1yyy";
        public static string PRIVATE_KEY ;
        public static Setting_model setting;



        public static void init() 
        {
            client_key = Security.getClientKey();
            PRIVATE_KEY = "aaa" + client_key + "bbb";
            app = new App_pref().getApp();
            Creator_db creator = new Creator_db();
            Log.removeOldLogs();


        }
    }

    



}

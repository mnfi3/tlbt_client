using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telbot.db;
using Telbot.helper;
using Telbot.license;
using Telbot.model;
using Telbot.storage;

namespace Telbot.system
{
    class G
    {
        public static string client_key;
        public static App_model app;
        public static Telegram_model telegram;
        public static bool isLoggedIn = false;
        public const string PUBLIC_KEY = "kkkF19BEE2EF1yyy";
        public static string PRIVATE_KEY ;
        public static Setting_model setting;

        //pouya
        public static string ApiHash { get; set; }
        public static string ApiId { get; set; }



        public static void init() 
        {
            
            client_key = Security.getClientKey();
            PRIVATE_KEY = "aaa" + client_key + "bbb";
            app = new App_pref().getApp();
            telegram = new Telegram_pref().getTelegram();
            Creator_db creator = new Creator_db();
            DB_helper.backup();
            Log.removeOldLogs();
            //app_directory_name = AppDomain.CurrentDomain.BaseDirectory;
            //app_directory_name = app_directory_name.Replace(@"\", "_");
            //app_directory_name = app_directory_name.Replace(":", "");
            //app_directory_name = app_directory_name.Replace(" ", "");

            ApiHash = "";
            ApiId = "";

        }

        public static string app_directory_name { 
            get {
                 
                string name = AppDomain.CurrentDomain.BaseDirectory;
                name = name.Replace(@"\", "_");
                name = name.Replace(":", "");
                name = name.Replace(" ", "");
                return name;
            }
        }



       

      
        
    }

    



}

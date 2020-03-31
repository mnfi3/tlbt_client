using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.system
{
    class Config
    {
        //public const string SITE_URL = "http://localhost:8080/telbot";
        public const string SITE_URL = "https://easy-bots.com";
        public const string APPLICATION_NAME = "telbot";
        public const string VERSION = "1.0.0.0";
        //public const string SQLITE_DB_CONNECTION = "Data Source=app_db.db;Version=3;UTF8Encoding=True;Compress=False;Password=" + G.PUBLIC_KEY + ";";
        public const string SQLITE_DB_CONNECTION = "Data Source=app_db.db;Version=3;UTF8Encoding=True;Compress=False;";
    }
}

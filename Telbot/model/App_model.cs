using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.model
{
    public class App_model
    {
        public int id { set; get; }
        public string user_name { set; get; }
        public string password { set; get; }
        public string token { set; get; }
        public string time_token { set; get; }


        public App_model()
        {
            id = 0;
            user_name = "";
            password = "";
            token = "";
            time_token = "";
        }


        public static App_model parse(JObject data)
        {
            JObject obj = data["app"].Value<JObject>();
            App_model app = new App_model();
            app.id = obj["id"].Value<int>();
            app.user_name = obj["user_name"].Value<string>();
            
            app.token = data["token"].Value<string>();
            app.time_token = data["time_token"].Value<string>();
            return app;
        }
    }
}

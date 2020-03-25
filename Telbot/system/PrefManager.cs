using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.license;
using Telbot.model;

namespace Telbot.system
{
    class PrefManager
    {
         //public const string KEY_DEVICE = "device";

        private static string json_string = null;
        private const string FOLDER = @"C:\telbot\";
        private const string FILE = @"C:\telbot\app.tlgbt";



        public PrefManager()
        {
            if (json_string != null) return;
            else init();
        }

        private void init(){
            if (!Directory.Exists(FOLDER)) System.IO.Directory.CreateDirectory(FOLDER);
            if (File.Exists(FILE))
            {
                read();
            }
            else
            {
                App_model app = new App_model();
                string json = JsonConvert.SerializeObject(app);
                save(json);
            }
            
        }

        private string read()
        {
            try
            {
                string json = File.ReadAllText(FILE);
                json_string = Crypt.DecryptString_128(json, G.PRIVATE_KEY);
                if (json_string.Contains("#"))
                {
                    App_model app = new App_model();
                    json = JsonConvert.SerializeObject(app);
                    save(json);
                }
                
            }
            catch (Exception e)
            {
                Log.e("read app data from file failed. error=" + e.ToString(), "PrefManager", "read");
            //    Device device = new Device();
            //    string json = JsonConvert.SerializeObject(device);
            //    save(json);
            }
            return json_string;
        }

        private void save(string json)
        {
            try
            {
                json_string = json;
                json = Crypt.EncryptString_128(json, G.PRIVATE_KEY);
                File.WriteAllText(FILE, json);
            }
            catch (Exception e) 
            {
                Log.e("save app data to file failed. error=" + e.ToString(), "PrefManager", "save");
            }
        }





        public bool isLoggedIn()
        {
            read();
            App_model app = JsonConvert.DeserializeObject<App_model>(json_string);
            if (app.id == 0) return false;
            return true;
        }



        public App_model getApp()
        {
            string json = read();
            App_model app = new App_model();
            try
            {
                if (JsonConvert.DeserializeObject<App_model>(json) != null)
                {
                    app = JsonConvert.DeserializeObject<App_model>(json);
                }
            }
            catch (JsonException e)
            {
                Log.e("json parsing error. error=" + e.ToString(), "PrefManager", "getApp");
            }
            return app;
        }

        public void saveApp(App_model app)
        {
            string json = JsonConvert.SerializeObject(app);
            save(json);
        }

        public void logoutDevice()
        {
            App_model app = new App_model();
            save(JsonConvert.SerializeObject(app));
        }
    }
}

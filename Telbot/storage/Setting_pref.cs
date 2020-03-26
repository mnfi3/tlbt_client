using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.license;
using Telbot.model;
using Telbot.system;

namespace Telbot.storage
{
    class Setting_pref
    {
        
        private static string json_string = null;
        private const string FOLDER = @"C:\" + Config.APPLICATION_NAME + @"\";
        private const string FILE = @"C:\" + Config.APPLICATION_NAME + @"\setting.tlgbt";



        public Setting_pref()
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
                Setting_model setting = new Setting_model();
                string json = JsonConvert.SerializeObject(setting);
                save(json);
            }
            
        }

        private string read()
        {
            try
            {
                string json = File.ReadAllText(FILE);
                json_string = Crypt.DecryptString_128(json, G.PRIVATE_KEY);
                if (json_string == "#")
                {
                    Setting_model setting = new Setting_model();
                    json = JsonConvert.SerializeObject(setting);
                    save(json);
                }
                
            }
            catch (Exception e)
            {
                Log.e("read setting data from file failed. error=" + e.ToString(), "Setting_pref", "read");
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
                Log.e("save setting data to file failed. error=" + e.ToString(), "Setting_pref", "save");
            }
        }





        public bool isFirstOpen()
        {
            read();
            Setting_model setting = JsonConvert.DeserializeObject<Setting_model>(json_string);
            if (setting.is_first_open == 1) return true;
            return false;
        }



        public Setting_model getSetting()
        {
            string json = read();
            Setting_model setting = new Setting_model();
            try
            {
                if (JsonConvert.DeserializeObject<Setting_model>(json) != null)
                {
                    setting = JsonConvert.DeserializeObject<Setting_model>(json);
                }
            }
            catch (JsonException e)
            {
                Log.e("json parsing error. error=" + e.ToString(), "Setting_pref", "getSetting");
            }
            return setting;
        }

        public void saveSetting(Setting_model setting)
        {
            string json = JsonConvert.SerializeObject(setting);
            save(json);
        }

        public void resetSetting()
        {
            Setting_model setting = new Setting_model();
            save(JsonConvert.SerializeObject(setting));
        }
    }
}

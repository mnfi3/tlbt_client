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
    class Telegram_pref
    {
        private static string json_string = null;
        private const string FOLDER = @"C:\" + Config.APPLICATION_NAME + @"\";
        private const string FILE = @"C:\" + Config.APPLICATION_NAME + @"\telegram.tlgbt";



        public Telegram_pref()
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
                Telegram_model telegram = new Telegram_model();
                string json = JsonConvert.SerializeObject(telegram);
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
                    Telegram_model telegram = new Telegram_model();
                    json = JsonConvert.SerializeObject(telegram);
                    save(json);
                }
                
            }
            catch (Exception e)
            {
                Log.e("read telegram data from file failed. error=" + e.ToString(), "Telegram_pref", "read");
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
                Log.e("save telegram data to file failed. error=" + e.ToString(), "Telegram_pref", "save");
            }
        }





        public bool isLoggedIn()
        {
            read();
            Telegram_model telegram = JsonConvert.DeserializeObject<Telegram_model>(json_string);
            if (telegram.is_logged_in == 0) return false;
            return true;
        }



        public Telegram_model getTelegram()
        {
            string json = read();
            Telegram_model telegram = new Telegram_model();
            try
            {
                if (JsonConvert.DeserializeObject<App_model>(json) != null)
                {
                    telegram = JsonConvert.DeserializeObject<Telegram_model>(json);
                }
            }
            catch (JsonException e)
            {
                Log.e("json parsing error. error=" + e.ToString(), "Telegram_pref", "getTelegram");
            }
            return telegram;
        }

        public void saveTelegram(Telegram_model telegram)
        {
            string json = JsonConvert.SerializeObject(telegram);
            save(json);
        }

        public void logoutTelegram()
        {
            Telegram_model telegram = new Telegram_model();
            save(JsonConvert.SerializeObject(telegram));
        }
    }
}

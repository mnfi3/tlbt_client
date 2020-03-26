using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.system
{
    class Log
    {

        private static string FOLDER = AppDomain.CurrentDomain.BaseDirectory + "Logs";

        private const string INFO = "INFO";
        private const string WARNIGN = "WARNIGN";
        private const string ERROR = "ERROR";





        public static void i(string message, string class_name = "", string method_name = "")
        {
            write(INFO, class_name, method_name, message);
        }

        public static void w(string message, string class_name = "", string method_name = "")
        {
            write(WARNIGN, class_name, method_name, message);
        }

        public static void e(string message, string class_name = "", string method_name = "")
        {
            write(ERROR, class_name, method_name, message);
        }







        private static void write(string type, string class_name, string method_name, string message)
        {
            string line = "";
            DateTime datetime = DateTime.Now;
            string date = datetime.ToString("yyyy_MM_dd");
            string time = datetime.ToString("yyyy-MM-dd  HH:mm:ss");

            line += time + "\t";
            line += "[" + type + "]" + "\t\t";
            line += "[" + class_name + "]" + ".";
            line += "[" + method_name + "]" + "\t";
            line += "<< " + message + " >>";

            saveToFile(line, Config.APPLICATION_NAME + "_" + date);
        }


        private static void saveToFile(string line, string file_name)
        {
            if (!Directory.Exists(FOLDER)) System.IO.Directory.CreateDirectory(FOLDER);

            //log path
            string path = FOLDER + @"\" + file_name + ".txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("file created ...\n");
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line + "\n");
            }



            //removeOldLogs();

        }






        public static void removeOldLogs()
        {
            if (!Directory.Exists(FOLDER))
            {
                System.IO.Directory.CreateDirectory(FOLDER);
                return;
            }
            DateTime datetime;


            List<string> last_days_logs_names = new List<string>();
            string date = "";

            for (int i = 0; i > -8; i--)
            {
                datetime = DateTime.Now.AddDays(i);
                date = datetime.ToString("yyyy_MM_dd");
                last_days_logs_names.Add(Config.APPLICATION_NAME + "_" + date + ".txt");
            }



            System.IO.DirectoryInfo di = new DirectoryInfo(FOLDER);
            foreach (FileInfo file in di.GetFiles())
            {
                bool is_find = false;
                foreach (string name in last_days_logs_names)
                {
                    if (file.Name.Contains(name))
                    {
                        is_find = true;
                        break;
                    }
                }

                try
                {
                    if (!is_find) file.Delete();
                }
                catch (Exception e) { }
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }


    }
}

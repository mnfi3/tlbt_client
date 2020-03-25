using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.system;

namespace Telbot.helper
{
    class DB_helper
    {
        public static void backup()
        {
            string fileName = "app_db.db";
            string sourcePath = AppDomain.CurrentDomain.BaseDirectory;
            string targetPath = @"C:\" + Config.APPLICATION_NAME;

            try
            {
                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.Directory.CreateDirectory(targetPath);
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch (IOException e)
            {
                Log.e("database backup failed.error=" + e.ToString(), "DB_helper", "backup");
            }

        }
       
    }
}

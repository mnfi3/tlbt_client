using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.storage;
using Telbot.system;

namespace Telbot.db
{
    class Creator_db
    {
        private string FILE = "app_db.db";

        private string[] tables = {
                                      "CREATE TABLE [mobiles] ( [id] INTEGER PRIMARY KEY AUTOINCREMENT, [number] BIGINT NOT NULL, [first_name] CHAR(100), [last_name] CHAR(100))"
                                  };



        public Creator_db()
        {
            if (!File.Exists(FILE))
            {
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();
                CreateTables(sqlite_conn);
                Log.i("database created", "Creator_db", "Creator_db");
            }
        }



        private SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=app_db.db;Version=3;UTF8Encoding=True;New=True;Compress=False;");
            sqlite_conn.SetPassword(G.PUBLIC_KEY);
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Log.e("database create connection error:" + ex.ToString(), "Creator_db", "CreateConnection");

            }
            return sqlite_conn;
        }

        private void CreateTables(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            foreach (string query in tables)
            {
                try
                {
                    sqlite_cmd.CommandText = query;
                    sqlite_cmd.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Log.e("database create table query=" + query + "\terror=" + e.ToString(), "Creator_db", "CreateTables");
                }
            }

        }
    }
}

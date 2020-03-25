using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.system;

namespace Telbot.db
{
    class Creator_db
    {
        private string FILE = "app_db.db";

        private string[] tables = {
                                      "CREATE TABLE [main].[categories] ( [id] integer,  [restaurant_id] integer, [name] nvarchar, [image] text)",


                                      "CREATE TABLE [main].[discounts] ([id] integer, [restaurant_id] integer, [code] text, [discount_percent] integer, [count] integer, [started_at] datetime, [invoked_at] datetime)",


                                      "CREATE TABLE [main].[foods] ([id] integer, [restaurant_id] integer, [category_id] integer, [is_side] integer, [name] text," +
                                      " [price] integer, [discount_percent] integer, [d_price] integer, [description] text, [image] text, [is_suggest] integer, [is_available] integer)",


                                      "CREATE TABLE [main].[orders] ([id] integer NOT NULL, [restaurant_id] integer, [order_number] nvarchar, [table_number] nchar, " +
                                      "[is_out] integer, [cost] integer, [d_cost] integer, [discount_id] integer, [time] nvarchar, [pan] nvarchar, [req_id] nvarchar," +
                                      "[serial_transaction] nvarchar, [terminal_no] nvarchar, [trace_number] nvarchar, [transaction_date] nvarchar, [transaction_time] nvarchar)",



                                      "CREATE TABLE [main].[order_items] ([id] integer, [order_id] integer NOT NULL, [food_id] integer NOT NULL, [cost] integer NOT NULL, [count] integer NOT NULL)",



                                      "CREATE TABLE [main].[printers] ([restaurant_id] integer, [name] nvarchar)",



                                      "CREATE TABLE [main].[receipt] ([num] nvarchar, [name] nvarchar, [price] nvarchar, [count] nvarchar, [cost] nvarchar)",
                                  };







        public Creator_db()
        {
            if (!File.Exists(FILE))
            {
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();
                CreateTables(sqlite_conn);
                Log.i("database created", "DB_creator", "DB_creator");
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
                Log.e("database create connection error:" + ex.ToString(), "DB_creator", "CreateConnection");

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
                    Log.e("database create table query=" + query + "\terror=" + e.ToString(), "DB_creator", "CreateTables");
                }
            }

        }
    }
}

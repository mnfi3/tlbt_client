using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.storage;
using Telbot.system;

namespace Telbot.db
{
    class DB
    {

        private string connetionString = null;
        private SQLiteConnection connection = null;
        private SQLiteCommand command = null;


        public DB()
        {
            connetionString = Config.SQLITE_DB_CONNECTION;
            connection = new SQLiteConnection(connetionString);
        }



        public SQLiteDataReader select(string query, Dictionary<string, string> values = null)
        {
            try
            {
                connection.Open();
                command = new SQLiteCommand(query, connection);
                if (values != null)
                {
                    foreach (string key in values.Keys)
                    {
                        command.Parameters.AddWithValue(key, values[key]);
                    }
                }
                SQLiteDataReader dataReader = command.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                Log.e("error in database query=" + query + "\terror=" + ex.ToString(), "DB", "select");
                return null;
            }
        }



        public int insert(string query, Dictionary<string, string> values = null)
        {
            int affected = 0;
            try
            {
                connection.Open();
                command = new SQLiteCommand(query, connection);
                if (values != null)
                {
                    foreach (string key in values.Keys)
                    {
                        if (values[key] != null)
                        {
                            command.Parameters.AddWithValue(key, values[key]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(key, "");
                        }
                    }
                }
                affected = command.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                Log.e("error in database query=" + query + "\terror=" + ex.ToString(), "DB", "insert");
                return -1;
            }

            return affected;
        }


        public int update(string query, Dictionary<string, string> values = null)
        {
            int affected = 0;
            try
            {
                connection.Open();
                command = new SQLiteCommand(query, connection);
                if (values != null)
                {
                    foreach (string key in values.Keys)
                    {
                        if (values[key] != null)
                        {
                            command.Parameters.AddWithValue(key, values[key]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(key, "");
                        }
                    }
                }
                affected = command.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                Log.e("error in database query=" + query + "\terror=" + ex.ToString(), "DB", "update");
                return -1;
            }

            return affected;
        }



        public int delete(string query, Dictionary<string, string> values = null)
        {
            int affected = 0;
            try
            {
                connection.Open();
                command = new SQLiteCommand(query, connection);
                if (values != null)
                {
                    foreach (string key in values.Keys)
                    {
                        if (values[key] != null)
                        {
                            command.Parameters.AddWithValue(key, values[key]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(key, "");
                        }
                    }
                }
                affected = command.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                Log.e("error in database query=" + query + "\terror=" + ex.ToString(), "DB", "delete");
                return -1;
            }

            return affected;
        }






        public void close()
        {
            if (command != null)
            {
                command.Dispose();
                connection.Close();
            }
        }



    }
}

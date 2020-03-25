using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.model;

namespace Telbot.db
{
    class Mobile_db:Model_db
    {
        public List<Mobile_model> getMobiles()
        {
            List<Mobile_model> mobiles = new List<Mobile_model>();
            SQLiteDataReader dataReader = db.select("select * from mobiles");
            if (dataReader != null)
            {
                Mobile_model mobile;
                while (dataReader.Read())
                {
                    mobile = new Mobile_model();
                    mobile.id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
                    mobile.number = dataReader.GetString(dataReader.GetOrdinal("number"));
                    mobile.first_name = dataReader.GetString(dataReader.GetOrdinal("first_name"));
                    mobile.last_name = dataReader.GetString(dataReader.GetOrdinal("last_name"));

                    mobiles.Add(mobile);
                }
            }

            db.close();
            return mobiles;
        }

        public int saveMobile(Mobile_model mobile)
        {
            values.Clear();
            values.Add("@number", mobile.number.ToString());
            values.Add("@first_name", mobile.first_name);
            values.Add("@last_name", mobile.last_name);
            return db.insert("insert into mobiles (number, first_name, last_name) values (@number, @first_name, @last_name)", values);
        }

        public List<Mobile_model> searchMobiles(Int32 search, Int32 from, Int32 to, string first_name = "", string last_name = "")
        {
            List<Mobile_model> mobiles = new List<Mobile_model>();
            string query = "select * from mobiles where 1=1 ";

            if (search != 0)
            {
                query += " and number like %@search% ";
                values.Add("@search", search.ToString());
            }
            if (from != 0)
            {
                query += " and number >= @from ";
                values.Add("@from", from.ToString());
            }
            if (from != 0)
            {
                query += " and number <= @to ";
                values.Add("@to", to.ToString());
            }
            if (first_name.Length > 0)
            {
                query += " and first_name like %@first_name% ";
                values.Add("@first_name", to.ToString());
            }
            if (last_name.Length > 0)
            {
                query += " and last_name like %@last_name% ";
                values.Add("@last_name", to.ToString());
            }


            SQLiteDataReader dataReader = db.select(query, values);
            if (dataReader != null)
            {
                Mobile_model mobile;
                while (dataReader.Read())
                {
                    mobile = new Mobile_model();
                    mobile.id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
                    mobile.number = dataReader.GetString(dataReader.GetOrdinal("number"));
                    mobile.first_name = dataReader.GetString(dataReader.GetOrdinal("first_name"));
                    mobile.last_name = dataReader.GetString(dataReader.GetOrdinal("last_name"));

                    mobiles.Add(mobile);
                }
            }

            db.close();
            return mobiles;
        }
    }
}

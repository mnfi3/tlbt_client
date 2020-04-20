using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.model;

namespace Telbot.db
{
    class Mobile_db:Base_db
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
                    mobile.number = dataReader.GetDouble(dataReader.GetOrdinal("number")).ToString();
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
            values.Add("@number", mobile.number);
            values.Add("@first_name", mobile.first_name);
            values.Add("@last_name", mobile.last_name);
            return db.insert("insert into mobiles (number, first_name, last_name) values (@number, @first_name, @last_name)", values);
        }

        public List<Mobile_model> searchMobiles(string search = "", string from = "", string to = "", string first_name = "", string last_name = "")
        {
            List<Mobile_model> mobiles = new List<Mobile_model>();
            values.Clear();
            string query = "select * from mobiles where 1=1 ";

            if (search.Length > 0)
            {
                query += " and number like @search ";
                values.Add("@search", "%" + search + "%");
            }
            if (from.Length > 0)
            {
                query += " and id >= @from ";
                values.Add("@from", from);
            }
            if (to.Length > 0)
            {
                query += " and id <= @to ";
                values.Add("@to", to);
            }
            if (first_name.Length > 0)
            {
                query += " and first_name like @first_name ";
                values.Add("@first_name", "%" + first_name + "%");
            }
            if (last_name.Length > 0)
            {
                query += " and last_name like @last_name ";
                values.Add("@last_name", "%" + last_name + "%");
            }


            SQLiteDataReader dataReader = db.select(query, values);
            if (dataReader != null)
            {
                Mobile_model mobile;
                while (dataReader.Read())
                {
                    mobile = new Mobile_model();
                    mobile.id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
                    mobile.number = dataReader.GetDouble(dataReader.GetOrdinal("number")).ToString();
                    mobile.first_name = dataReader.GetString(dataReader.GetOrdinal("first_name"));
                    mobile.last_name = dataReader.GetString(dataReader.GetOrdinal("last_name"));

                    mobiles.Add(mobile);
                }
            }

            db.close();
            return mobiles;
        }


        public void removeMobiles(List<Mobile_model> mobiles)
        {
            foreach (Mobile_model mobile in mobiles)
            {
                values.Clear();
                values.Add("@number", mobile.number);
                db.delete("delete from mobiles where number = @number", values);
            }
        }
        public Mobile_model findMobile(string number)
        {
            Mobile_model mobile = null;
            values.Clear();
            values.Add("@number", number);
            SQLiteDataReader dataReader = db.select("select * from mobiles where number like @number", values);
            if (dataReader != null)
            {
                while (dataReader.Read())
                {

                    mobile = new Mobile_model();
                    mobile.id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
                    mobile.number = dataReader.GetDouble(dataReader.GetOrdinal("number")).ToString();
                    mobile.first_name = dataReader.GetString(dataReader.GetOrdinal("first_name"));
                    mobile.last_name = dataReader.GetString(dataReader.GetOrdinal("last_name"));

                }
            }
           

            db.close();
            return mobile;
            
        }
    }
}

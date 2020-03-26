using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.db
{
    class Base_db
    {
        protected DB db;
        protected Dictionary<string, string> values;
        public Base_db()
        {
            this.db = new DB();
            values = new Dictionary<string, string>();
        }
    }
}

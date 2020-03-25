using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbot.db
{
    class Model_db
    {
        protected DB db;
        protected Dictionary<string, string> values;
        public Model_db()
        {
            this.db = new DB();
            values = new Dictionary<string, string>();
        }
    }
}

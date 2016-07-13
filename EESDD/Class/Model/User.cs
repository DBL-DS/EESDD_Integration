using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    abstract class User
    {
        private string name;
        private string realName;
        private DateTime lastDate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        public DateTime LastDate
        {
            get { return lastDate; }
            set { lastDate = value; }
        }

    }
}

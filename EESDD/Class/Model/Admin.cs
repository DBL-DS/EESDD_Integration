using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Admin:User
    {
        string grantUserName;

        public string GrantUserName
        {
            get { return grantUserName; }
            set { grantUserName = value; }
        }
    }
}

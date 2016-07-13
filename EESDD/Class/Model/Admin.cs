using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    class Admin:User
    {
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}

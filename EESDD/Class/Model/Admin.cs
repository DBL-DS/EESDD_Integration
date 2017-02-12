using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    public class Admin:User
    {
        public Admin() { }

        public Admin(
            string name,
            string password,
            string realName,
            string grantUserName)
        {
            this.Name = name;
            this.Password = password;
            this.RealName = realName;
            this.grantUserName = grantUserName;
        }

        string grantUserName;

        public string GrantUserName
        {
            get { return grantUserName; }
            set { grantUserName = value; }
        }
    }
}

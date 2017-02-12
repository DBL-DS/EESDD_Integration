using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Model
{
    public enum UserGroup      // 数据库表名应与此保持一致（小写）
    {
        ADMIN,
        REGULAR
    }

    public enum UserVariable
    {
        Name,
        Password,
        RealName,
        RegDate,
        LastDate,
        Group,
        GrantUserName,
        Gender,
        Height,
        Weight,
        Age,
        DriAge,
        Career,
        Contact
    }

    public abstract class User
    {
        private string name;
        private string password;
        private string realName;
        private DateTime regDate;
        private DateTime lastDate;
        private UserGroup group;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        public DateTime RegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }

        public DateTime LastDate
        {
            get { return lastDate; }
            set { lastDate = value; }
        }

        public UserGroup Group
        {
            get { return group; }
            set { group = value; }
        }

    }
}

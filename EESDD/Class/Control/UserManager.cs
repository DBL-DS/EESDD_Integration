using EESDD.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    public enum RegisteState
    {
        USEREXIST,

    }

    class UserManager
    {
        public UserManager()
        {
            InitDbManger();
        }

        private void InitDbManger()
        {
            dbManger = new UserDBManger();
            dbManger.ConnectDB(FileManger.GetPath("database", "db"));
        }

        private User user;
        private UserDBManger dbManger;

        public LoginState Login(string username, string password, UserGroup group)
        {
            if (group == UserGroup.ADMIN && password.Equals(""))
                return LoginState.NEEDPASSWORD;

            Tuple<LoginState, User> result = dbManger.ValidateUser(username, password, group);
            if (result.Item1 == LoginState.SUCCESS)
                user = result.Item2;

            return result.Item1;
        }

        private bool Registe()
        {

            return false;
        }
    }
}

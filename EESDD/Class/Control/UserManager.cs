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
        SUCCESS,
        VALIDATED,
        GRANTUSERNOTEXIST,
        NAMEEMPTY,
        ADMINPEEMPTY,
        REALNAMEEMPTY,
        GENDEREMPTY,
        HEIGHTEMPTY,
        WEIGHTEMPTY,
        AGEEMPTY,
        DRIAGEEMPT,
        CAREEREMPTY,
        CONTACTEMPTY,
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

        private RegisteState Registe(User user)
        {

            return RegisteState.SUCCESS;
        }

        private RegisteState Validate(User user, string variable)
        {
            switch (variable)
            {

            }
            if (user.Name.Equals(""))
                return RegisteState.NAMEEMPTY;

            if (user.Password.Equals(""))
                return RegisteState.ADMINPEEMPTY;

            if (user.RealName.Equals(""))
                return RegisteState.REALNAMEEMPTY;

            if (user.Group == UserGroup.REGULAR)
            {
                Regular regular = user as Regular;

                if (regular.Gender.Equals(""))
                    return RegisteState.GENDEREMPTY;

                if (regular.Career.Equals(""))
                    return RegisteState.CAREEREMPTY;

                if (regular.Contact.Equals(""))
                    return RegisteState.CONTACTEMPTY;
            }

            if (dbManger.GetUser(user.Name, user.Group) != null)
                return RegisteState.USEREXIST;

            if (user.Group == UserGroup.ADMIN
                && dbManger.GetUser((user as Admin).GrantUserName, UserGroup.ADMIN)
                == null)
                return RegisteState.GRANTUSERNOTEXIST;

            return RegisteState.VALIDATED;
        }
    }
}

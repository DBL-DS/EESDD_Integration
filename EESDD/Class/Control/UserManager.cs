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
        DBFAIELD,
        VALIDATED,
        GRANTUSERNOTEXIST,
        NAMEEMPTY,
        PASSWORDPEEMPTY,
        REALNAMEEMPTY,
        GENDEREMPTY,
        HEIGHTEMPTY,
        WEIGHTEMPTY,
        AGEEMPTY,
        DRIAGEEMPTY,
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
        private User registeUser;
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

        public void RegisteStart(UserGroup group)
        {
            switch (group)
            {
                case UserGroup.ADMIN:
                    registeUser = new Admin();
                    registeUser.Group = UserGroup.ADMIN;
                    (registeUser as Admin).GrantUserName = user.Name;
                    break;
                case UserGroup.REGULAR:
                    registeUser = new Regular();
                    registeUser.Group = UserGroup.REGULAR;
                    break;
                default:
                    break;
            }
        }

        public RegisteState RegisteAdd(UserVariable variable, string value)
        {
            switch (variable)
            {
                case UserVariable.Name:
                    if (value.Equals(""))
                        return RegisteState.NAMEEMPTY;
                    if (dbManger.GetUser(value, registeUser.Group) != null)
                        return RegisteState.USEREXIST;
                    registeUser.Name = value;
                    break;
                case UserVariable.Password:
                    if (registeUser.Group 
                        == UserGroup.ADMIN && value.Equals(""))
                        return RegisteState.PASSWORDPEEMPTY;
                    registeUser.Password = value;
                    break;
                case UserVariable.RealName:
                    if (value.Equals(""))
                        return RegisteState.REALNAMEEMPTY;
                    registeUser.RealName = value;
                    break;
                case UserVariable.Gender:
                    if (value.Equals(""))
                        return RegisteState.GENDEREMPTY;
                    (registeUser as Regular).Gender = value;
                    break;
                case UserVariable.Height:
                    if (value.Equals(""))
                        return RegisteState.HEIGHTEMPTY;
                    (registeUser as Regular).Height = float.Parse(value);
                    break;
                case UserVariable.Weight:
                    if (value.Equals(""))
                        return RegisteState.WEIGHTEMPTY;
                    (registeUser as Regular).Weight = float.Parse(value);
                    break;
                case UserVariable.Age:
                    if (value.Equals(""))
                        return RegisteState.AGEEMPTY;
                    (registeUser as Regular).Age = int.Parse(value);
                    break;
                case UserVariable.DriAge:
                    if (value.Equals(""))
                        return RegisteState.DRIAGEEMPTY;
                    (registeUser as Regular).DriAge = int.Parse(value);
                    break;
                case UserVariable.Career:
                    if (value.Equals(""))
                        return RegisteState.CAREEREMPTY;
                    (registeUser as Regular).Career = value;
                    break;
                case UserVariable.Contact:
                    if (value.Equals(""))
                        return RegisteState.CONTACTEMPTY;
                    (registeUser as Regular).Contact = value;
                    break;
                default:
                    break;
            }
            return RegisteState.VALIDATED;
        }

        public RegisteState RegisteEnd()
        {
            if (dbManger.AddUser(registeUser))
                return RegisteState.SUCCESS;
            else
                return RegisteState.DBFAIELD;
        }

    }
}

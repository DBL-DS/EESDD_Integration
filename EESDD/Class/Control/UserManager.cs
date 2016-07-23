using EESDD.Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    public enum RegisterState
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
        private User registerUser;
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

        public void RegisterStart(UserGroup group)
        {
            switch (group)
            {
                case UserGroup.ADMIN:
                    registerUser = new Admin();
                    registerUser.Group = UserGroup.ADMIN;
                    (registerUser as Admin).GrantUserName = user.Name;
                    break;
                case UserGroup.REGULAR:
                    registerUser = new Regular();
                    registerUser.Group = UserGroup.REGULAR;
                    break;
                default:
                    break;
            }
        }

        public RegisterState RegisterAdd(UserVariable variable, string value)
        {
            switch (variable)
            {
                case UserVariable.Name:
                    if (value.Equals(""))
                        return RegisterState.NAMEEMPTY;
                    if (dbManger.GetUser(value, registerUser.Group) != null)
                        return RegisterState.USEREXIST;
                    registerUser.Name = value;
                    break;
                case UserVariable.Password:
                    if (registerUser.Group 
                        == UserGroup.ADMIN && value.Equals(""))
                        return RegisterState.PASSWORDPEEMPTY;
                    registerUser.Password = value;
                    break;
                case UserVariable.RealName:
                    if (value.Equals(""))
                        return RegisterState.REALNAMEEMPTY;
                    registerUser.RealName = value;
                    break;
                case UserVariable.Gender:
                    if (value.Equals(""))
                        return RegisterState.GENDEREMPTY;
                    (registerUser as Regular).Gender = value;
                    break;
                case UserVariable.Height:
                    if (value.Equals(""))
                        return RegisterState.HEIGHTEMPTY;
                    (registerUser as Regular).Height = float.Parse(value);
                    break;
                case UserVariable.Weight:
                    if (value.Equals(""))
                        return RegisterState.WEIGHTEMPTY;
                    (registerUser as Regular).Weight = float.Parse(value);
                    break;
                case UserVariable.Age:
                    if (value.Equals(""))
                        return RegisterState.AGEEMPTY;
                    (registerUser as Regular).Age = int.Parse(value);
                    break;
                case UserVariable.DriAge:
                    if (value.Equals(""))
                        return RegisterState.DRIAGEEMPTY;
                    (registerUser as Regular).DriAge = int.Parse(value);
                    break;
                case UserVariable.Career:
                    if (value.Equals(""))
                        return RegisterState.CAREEREMPTY;
                    (registerUser as Regular).Career = value;
                    break;
                case UserVariable.Contact:
                    if (value.Equals(""))
                        return RegisterState.CONTACTEMPTY;
                    (registerUser as Regular).Contact = value;
                    break;
                default:
                    break;
            }
            return RegisterState.VALIDATED;
        }

        public RegisterState RegisterEnd()
        {
            if (dbManger.AddUser(registerUser))
                return RegisterState.SUCCESS;
            else
                return RegisterState.DBFAIELD;
        }

    }
}

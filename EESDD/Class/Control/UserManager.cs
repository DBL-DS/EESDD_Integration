using EESDD.Class.Model;
using EESDD.Module.Encryption;
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
        DBFAILED,
        VALIDATED,
        GRANTUSERNOTEXIST,
        NAMEEMPTY,
        PASSWORDEMPTY,
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
            InitDBManger();
        }

        private void InitDBManger()
        {
            dbManger = new UserDBManager();
            dbManger.ConnectDB(FileManager.GetPath("database", "db"));
            expManger = new ExpManager();
        }

        private User user;
        private User registerUser;
        private UserDBManager dbManger;
        private ExpManager expManger;
        private RegisterState currentState;

        public LoginState Login(string username, string password, UserGroup group)
        {
            if (group == UserGroup.ADMIN && password.Equals(""))
                return LoginState.NEEDPASSWORD;

            Tuple<LoginState, User> result 
                = dbManger.ValidateUser(username, password, group);
            if (result.Item1 == LoginState.SUCCESS)
            {
                user = result.Item2;
                if (group == UserGroup.REGULAR)
                    expManger.Load((user as Regular).ExpFile);
            }

            return result.Item1;
        }

        public void RegisterStart(UserGroup group)
        {
            currentState = RegisterState.VALIDATED;
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
                    {
                        UpdateRegisterState(RegisterState.NAMEEMPTY);
                        return RegisterState.NAMEEMPTY;
                    }
                    if (dbManger.GetUser(value, registerUser.Group) != null)
                    {
                        UpdateRegisterState(RegisterState.USEREXIST);
                        return RegisterState.USEREXIST;
                    }
                    registerUser.Name = value;
                    if (registerUser.Group == UserGroup.REGULAR)
                        (registerUser as Regular).ExpFile 
                            = ExpManager.GetFileName(registerUser.Name);
                    break;
                case UserVariable.Password:
                    if (registerUser.Group 
                        == UserGroup.ADMIN && value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.PASSWORDEMPTY);
                        return RegisterState.PASSWORDEMPTY;
                    }
                    registerUser.Password = value;
                    break;
                case UserVariable.RealName:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.REALNAMEEMPTY);
                        return RegisterState.REALNAMEEMPTY;
                    }
                    registerUser.RealName = value;
                    break;
                case UserVariable.Gender:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.GENDEREMPTY);
                        return RegisterState.GENDEREMPTY;
                    }
                    (registerUser as Regular).Gender = value;
                    break;
                case UserVariable.Height:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.HEIGHTEMPTY);
                        return RegisterState.HEIGHTEMPTY;
                    }
                    (registerUser as Regular).Height = float.Parse(value);
                    break;
                case UserVariable.Weight:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.WEIGHTEMPTY);
                        return RegisterState.WEIGHTEMPTY;
                    }
                    (registerUser as Regular).Weight = float.Parse(value);
                    break;
                case UserVariable.Age:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.AGEEMPTY);
                        return RegisterState.AGEEMPTY;
                    }
                    (registerUser as Regular).Age = int.Parse(value);
                    break;
                case UserVariable.DriAge:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.DRIAGEEMPTY);
                        return RegisterState.DRIAGEEMPTY;
                    }
                    (registerUser as Regular).DriAge = int.Parse(value);
                    break;
                case UserVariable.Career:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.CAREEREMPTY);
                        return RegisterState.CAREEREMPTY;
                    }
                    (registerUser as Regular).Career = value;
                    break;
                case UserVariable.Contact:
                    if (value.Equals(""))
                    {
                        UpdateRegisterState(RegisterState.CONTACTEMPTY);
                        return RegisterState.CONTACTEMPTY;
                    }
                    (registerUser as Regular).Contact = value;
                    break;
                default:
                    break;
            }
            return RegisterState.VALIDATED;
        }

        private void UpdateRegisterState(RegisterState state)
        {
            if (currentState == RegisterState.VALIDATED)
                currentState = state;
        }

        public RegisterState RegisterEnd()
        {
            if (currentState == RegisterState.VALIDATED)
            {
                if (dbManger.AddUser(registerUser))
                    currentState = RegisterState.SUCCESS;
                else
                    currentState = RegisterState.DBFAILED;
            }
            registerUser = null;
            return currentState;
        }

        public User User { get { return user; } }

        public void LogOut()
        {
            user = null;
        }

        public bool UpdateUser(User user)
        {
            return dbManger.UpdateUserInfo(user);
        }

        public bool UpdatePassword(string oldP, string newP)
        {
            if (Encryptor.GetMD5(oldP) == user.Password)
                return dbManger.UpdatePassword(user.Name, newP, user.Group);
            else
                return false;
        }

    }
}

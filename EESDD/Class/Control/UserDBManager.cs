using EESDD.Class.Model;
using EESDD.Module.Encryption;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Class.Control
{
    enum LoginState
    {
        NOTEXIST,
        WRONGPASSWORD,
        NEEDPASSWORD,
        SUCCESS
    }
    class UserDBManager
    {
        public UserDBManager()
        {

        }

        private SQLiteConnection connection;

        private void OpenConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Open();
            }
        }

        public bool ConnectDB(string dbPath)
        {
            if (!File.Exists(dbPath) 
                && !CreateDB(dbPath, 
                FileManager.GetPath("database", "create_sql")))
                return false;

            connection = new SQLiteConnection("Data Source=" 
                + dbPath + ";Version=3;");

            return true;
        }
        public bool CreateDB(string dbPath, string sqlPath)
        {
            SQLiteConnection.CreateFile(dbPath);
            connection = new SQLiteConnection("Data Source=" 
                + dbPath + ";Version=3;");

            string sql = File.ReadAllText(sqlPath);

            return ExecuteNonQuery(sql);
        }

        public Tuple<LoginState, User> ValidateUser(string name,
            string password, UserGroup group)
        {
            LoginState state;
            User user = GetUser(name, group);

            if (user == null)
                state = LoginState.NOTEXIST;
            else if (Encryptor.GetMD5(password).Equals(user.Password))
            {
                state = LoginState.SUCCESS;
                UpdateLastDate(name, group);
            }
            else
                state = LoginState.WRONGPASSWORD;

            return new Tuple<LoginState,User>(state, user);
        }

        private bool UpdateLastDate(string name, UserGroup group)
        {
            string sql = "update " + group 
                + " set lastDate = (datetime('now','localtime'))" 
                + " where name = '" + name + "'";

            return ExecuteNonQuery(sql);
        }

        public User GetUser(string name, UserGroup group)
        {
            string sql = "select * from " + group
                + " where name = '" + name + "'";

            List<User> users = GetUsers(sql, group);

            if (users != null && users.Count != 0)
                return users[0];
            else
                return null;
        }

        public List<User> GetAllRegulars()
        {
            string sql = "select * from " + UserGroup.REGULAR;
            List<User> users = GetUsers(sql, UserGroup.REGULAR);

            return users;
        }

        public List<User> GetAllGrantedUsers(string name)
        {
            string sql = "select * from " + UserGroup.ADMIN
                + " where grantUserName = " + name;
            List<User> users = GetUsers(sql, UserGroup.ADMIN);

            return users;
        }

        private List<User> GetUsers(string sql, UserGroup group)
        {
            OpenConnection();
            SQLiteDataReader reader = ExecuteQuery(sql);

            if (reader != null && !reader.HasRows)
            {
                reader.Close();
                connection.Close();
                return null;
            }

            List<User> users = new List<User>();
            while (reader.Read())
            {
                User user;
                switch (group)
                {
                    case UserGroup.ADMIN:
                        user = new Admin();
                        (user as Admin).Group = UserGroup.ADMIN;
                        (user as Admin).GrantUserName = reader["grantUserName"] as string;
                        break;
                    case UserGroup.REGULAR:
                        user = new Regular();
                        Regular regular = user as Regular;
                        regular.Group = UserGroup.REGULAR;
                        regular.Gender = reader["gender"] as string;
                        regular.Height = GetFloat(reader["height"]);
                        regular.Weight = GetFloat(reader["weight"]);
                        regular.Age = GetInt(reader["age"]);
                        regular.DriAge = GetInt(reader["driAge"]);
                        regular.Career = reader["career"] as string;
                        regular.Contact = reader["contact"] as string;
                        regular.ExpFile = reader["exp"] as string;
                        break;
                    default:
                        user = null;
                        break;
                }

                user.Name = reader["name"] as string;
                user.Password = reader["password"] as string;
                user.RealName = reader["realName"] as string;
                user.RegDate = (DateTime)reader["regDate"];
                user.LastDate = (DateTime)reader["lastDate"];

                users.Add(user);
            }

            reader.Close();
            connection.Close();

            return users;
        }

        public bool AddUser(User user)
        {
            string sql;
            switch (user.Group)
            {
                case UserGroup.ADMIN:
                    Admin admin = user as Admin;
                    sql = "insert into " + admin.Group
                        + " (name, password, realName, grantUserName) values ('"
                        + admin.Name + "', '"
                        + Encryptor.GetMD5(admin.Password) + "', '"
                        + admin.RealName + "', '"
                        + admin.GrantUserName + "') ";
                    break;
                case UserGroup.REGULAR:
                    Regular regular = user as Regular;
                    sql = "insert into " + regular.Group
                        + " (name, password, realName, gender, height, weight, " 
                        + "age, driAge, career, contact, exp) values ('"
                        + regular.Name + "', '"
                        + Encryptor.GetMD5(regular.Password) + "', '"
                        + regular.RealName + "', '"
                        + regular.Gender + "', "
                        + regular.Height + ", "
                        + regular.Weight + ", "
                        + regular.Age + ", "
                        + regular.DriAge + ", '"
                        + regular.Career + "', '"
                        + regular.Contact + "', '"
                        + ExpManager.GetFileName(regular.Name) + "') ";
                    break;
                default:
                    return false;
            }

            return ExecuteNonQuery(sql);
        }

        public bool UpdateUserInfo(string name, User user)
        {
            string sql;
            switch (user.Group)
            {
                case UserGroup.ADMIN:
                    Admin admin = user as Admin;
                    sql = "update " + admin.Group + " set "
                        + "realName = '" + admin.RealName + "' "
                        + "where name = " + name;
                    break;
                case UserGroup.REGULAR:
                    Regular regular = user as Regular;
                    sql = "update " + regular.Group + " set "
                        + "realName = '" + regular.RealName + "', "
                        + "gender = '" + regular.Gender + "', "
                        + "height = " + regular.Height + ", "
                        + "weight = " + regular.Weight + ", "
                        + "age = " + regular.Age + ", "
                        + "driAge = " + regular.DriAge + ", "
                        + "career = '" + regular.Career + "',"
                        + "contact = '" + regular.Contact + "' "
                        + "where name = " + name;
                    break;
                default:
                    return false;
            }

            return ExecuteNonQuery(sql);
        }

        public bool UpdatePassword(string name, string password, UserGroup group)
        {
            string sql = "update " + group 
                + " set password = '" + Encryptor.GetMD5(password) 
                + "' where name = " + name;

            return ExecuteNonQuery(sql);
        }

        private SQLiteDataReader ExecuteQuery(string sql)
        {
            SQLiteDataReader reader;
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                reader = command.ExecuteReader();
            }
            catch (Exception)
            {
                return null;
            }

            return reader;
        }

        private bool ExecuteNonQuery(string sql)
        {
            try
            {
                OpenConnection();
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private float GetFloat(object value)
        {
            return (float)(double)value;
        }

        private int GetInt(object value)
        {
            return (int)(long)value;
        }
    }
}

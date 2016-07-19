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
    class UserDBManger
    {
        public UserDBManger()
        {

        }

        private SQLiteConnection connection;

        public bool ConnectDB(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");

            return false;
        }
        public bool CreateDB(string dbPath, string sqlPath)
        {
            SQLiteConnection.CreateFile(dbPath);
            connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            connection.Open();

            try
            {
                string sql = File.ReadAllText(sqlPath);
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                connection.Close();
                return false;
            }
        }

        public Tuple<LoginState, User> ValidateUser(string name, string password, UserGroup group)
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

        private void UpdateLastDate(string name, UserGroup group)
        {
            connection.Open();

            string sql = "update " + group + " set lastDate = (datetime('now','localtime'))" 
                + " where name = '" + name + "'";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public User GetUser(string name, UserGroup group)
        {
            connection.Open();

            string sql = "select * from " + group.ToString()
                + " where name = '" + name + "'";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            User user = GetUser(reader, group);
            reader.Close();

            connection.Close();

            return user;
        }

        private User GetUser(SQLiteDataReader reader, UserGroup group)
        {
            if (reader != null && ! reader.HasRows)
                return null;

            reader.Read();

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
                    regular.Height = (float)reader["height"];
                    regular.Weight = (float)reader["weight"];
                    regular.Age = (int)reader["age"];
                    regular.DriAge = (int)reader["driAge"];
                    regular.Career = reader["career"] as string;
                    regular.Contact = reader["contact"] as string;
                    regular.ExpFile = reader["exp"] as string;
                    break;
                default:
                    return null;
            }
            
            user.Name = reader["name"] as string;
            user.Password = reader["password"] as string;
            user.RealName = reader["realName"] as string;
            user.RegDate = (DateTime)reader["regDate"];
            user.LastDate = (DateTime)reader["lastDate"];

            return user;
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
                        + admin.Password + "', '"
                        + admin.RealName + "', '"
                        + admin.GrantUserName + "') ";
                    break;
                case UserGroup.REGULAR:
                    Regular regular = user as Regular;
                    sql = "insert into " + regular.Group
                        + " (name, password, realName, grantUserName) values ('"
                        + regular.Name + "', '"
                        + regular.Password + "', '"
                        + regular.RealName + "', '"
                        + regular.Gender + "', "
                        + regular.Height + ", "
                        + regular.Weight + ", "
                        + regular.Age + ", "
                        + regular.DriAge + ", '"
                        + regular.Career + "', '"
                        + regular.Contact + "', '"
                        + Encryptor.GetMD5(regular.Name) + ".exp') ";
                    break;
            }
            return false;
        }
    }
}

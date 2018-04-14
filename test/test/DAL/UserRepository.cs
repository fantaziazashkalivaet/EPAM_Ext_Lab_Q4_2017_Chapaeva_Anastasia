using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using test.DAL.Models;

namespace test.DAL
{
    public class UserRepository : IUserRepository
    {
        private DbProviderFactory factory;
        private string connectionString;
        private const int userNotExist = 0;

        public UserRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings["smthg"];
            this.connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public bool CheckUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT UserID, Login, PasswordHash, StatusID FROM Users " +
                                      "WHERE @Login = Login AND @PasswordHash = PasswordHash ";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                // добавить проверку
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
            }

            return false;
        }

        public int CreateUser(User user)
        {
            int userId = userNotExist;

            // проверка на повторение имени пользователя
            if (SearchUserByLogin(user.Login).UserID != userNotExist)
            {
                return userId;
            }

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "INSERT INTO Users " +
                    "([Login], [PasswordHash], [StatusID]) " +
                    "VALUES (@Login, @PasswordHash, @StatusID);" +
                    "SET @INSERTED_ID=SCOPE_IDENTITY()";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@StatusID", user.StatusID);

                var uID = new SqlParameter();
                uID.ParameterName = "INSERTED_ID";
                uID.Size = 15;
                uID.Direction = ParameterDirection.Output;
                command.Parameters.Add(uID);

                // добавить проверку
                connection.Open();
                command.ExecuteNonQuery();

                userId = int.Parse(uID.Value.ToString());
            }

            return userId;
        }

        public User SearchUserByID(int id)
        {
            User user = null;

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT UserID, Login, PasswordHash, StatusID FROM [Users] " +
                                      "WHERE @ID = UserID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.UserID = (int)reader["UserID"];
                        user.Login = reader["Login"] as string;
                        user.PasswordHash = reader["PasswordHash"] as string;
                        user.StatusID = (int)reader["StatusID"];
                    }
                }
            }

            return user;
        }

        public User SearchUserByLogin(string login)
        {
            var user = new Models.User();

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT UserID, Login, PasswordHash, StatusID FROM [Users] " +
                                      "WHERE LOWER(@Login) = LOWER(Login)";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Login", string.IsNullOrEmpty(login) ? DBNull.Value : (object)login);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.UserID = (int)reader["UserID"];
                        user.Login = reader["Login"] as string;
                        user.PasswordHash = reader["PasswordHash"] as string;
                        user.StatusID = (int)reader["StatusID"];
                    }
                }
            }

            return user;
        }

        public string GetHashString(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);

            var csp = new MD5CryptoServiceProvider();
            var byteHash = csp.ComputeHash(bytes);
            var hash = new StringBuilder();

            foreach (var b in byteHash)
            {
                hash.Append(b.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
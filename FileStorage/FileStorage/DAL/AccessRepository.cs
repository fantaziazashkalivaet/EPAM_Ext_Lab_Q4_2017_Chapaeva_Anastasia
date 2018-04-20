namespace FileStorage.DAL
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using Interfaces;
    using Models;

    public class AccessRepository : IAccessRepository
    {
        private DbProviderFactory factory;
        private string connectionString;

        public AccessRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings["FilesDatabase"];
            this.connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        /// <summary>
        /// Сменить доступ к документу
        /// </summary>
        public void ChangeAccess(int documentID, DocumentAccess newAccess, DocumentAccess oldAccess)
        {
            if (oldAccess == DocumentAccess.Partial && newAccess != DocumentAccess.Partial)
            {
                RemoveDocumentFromM2M(documentID);
            }

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = string.Format(
                    "UPDATE Documents SET AccessID = '{0}' WHERE DocumentID = '{1}'",
                    (int)newAccess,
                    documentID);
                command.CommandType = CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Открывает/закрывает доступ пользователя к документу
        /// </summary>
        public void ChangePartialAccessToUser(int userID, int documentID)
        {
            string commandString;
            if (CheckUserAccess(userID, documentID))
            {
                commandString = string.Format(
                    "DELETE FROM[M2M_Users_Documents] WHERE DocumentID = {0} AND UserID = {1}",
                    documentID,
                    userID);
            }
            else
            {
                commandString = string.Format(
                    "INSERT INTO M2M_Users_Documents ([UserID], [DocumentID]) VALUES ({0}, {1})",
                    userID,
                    documentID);
            }

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = commandString;
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool CheckUserAccess(int userID, int docID)
        {
            bool check = false;
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = string.Format(
                                        "SELECT * FROM [M2M_Users_Documents] WHERE UserID={0} AND DocumentID={1}", userID, docID);
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        check = true;
                    }
                }
            }

            return check;
        }

        public IEnumerable<DocumentAccess> GetAccess()
        {
            var access = new List<DocumentAccess>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT AccessID, Access FROM [Access]";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        access.Add((DocumentAccess)reader["AccessID"]);
                    }
                }
            }

            return access;
        }

        /// <summary>
        /// Возвращает всех пользователей, имеющих доступ к документу
        /// </summary>
        public IEnumerable<User> UsersWithAccess(int docID)
        {
            var users = new List<User>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT U.UserID, Login " +
                                                  " FROM [M2M_Users_Documents] as M " +
                                                  " JOIN [Users] as U " +
                                                  " ON M.UserID = U.UserID ";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            UserID = (int)reader["UserID"],
                            Login = reader["Login"] as string
                        });
                    }
                }
            }

            return users;
        }

        private void RemoveDocumentFromM2M(int documentID)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "DELETE FROM [M2M_Users_Documents] WHERE DocumentID = @DocumentID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@DocumentID", documentID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
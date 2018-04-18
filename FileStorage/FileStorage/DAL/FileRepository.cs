using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
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

namespace FileStorage.DAL
{
    public class FileRepository : IFileRepository
    {
        private DbProviderFactory factory;
        private string connectionString;

        public FileRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings["FilesDatabase"];
            this.connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        /// <summary>
        /// Открыть пользователю доступ к документу
        /// </summary>
        public void AddPartialAccessToUser(int userID, int documentID)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "INSERT INTO M2M_Users_Documents " +
                                      "([UserID], [DocumentID]) " +
                                      "VALUES (@userID, documentID)";
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@documentID", documentID);
                command.CommandType = CommandType.Text;

                // добавить проверку на ошибки
                connection.Open();
                command.ExecuteNonQuery();
            }
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
                command.CommandText = "UPDATE Documents SET AccessID = @AccessID " +
                                      "WHERE DocumentID = @DocumentID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@AccessID", (int)newAccess);
                command.Parameters.AddWithValue("@DocumentID", documentID);

                // проверка
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Добавление документа в базу
        /// </summary>
        public void CreateDocument(Document doc)
        {
            string docId;

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "INSERT INTO Documents " +
                    "([Title], [Doc], [UserID], [AccessID], [Date], [TagID]) " +
                    "VALUES (@Title, @Doc, @UserID, @AccessID, @Date, @TagID);" +
                    "SET @INSERTED_ID=SCOPE_IDENTITY()";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Title", doc.Title);
                command.Parameters.AddWithValue("@Doc", doc.Doc);
                command.Parameters.AddWithValue("@UserID", doc.UserID);
                command.Parameters.AddWithValue("@AccessID", (int)doc.AccessID);
                command.Parameters.AddWithValue("@Date", doc.Date);
                command.Parameters.AddWithValue("@TagID", doc.Tag.TagID);

                var dID = new SqlParameter();
                dID.ParameterName = "INSERTED_ID";
                dID.Size = 15;
                dID.Direction = ParameterDirection.Output;
                command.Parameters.Add(dID);

                // добавить проверку
                connection.Open();
                command.ExecuteNonQuery();

                docId = dID.Value.ToString();
                var hash = GetHashString(docId);

                AddHashInDocuments(Convert.ToInt32(docId), hash);
            }
        }

        /// <summary>
        /// Удаление документы из базы
        /// </summary>
        public void DeleteDocument(int documentID, DocumentAccess access)
        {
            // удаление документа из Documents
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "DELETE FROM Documents " +
                    "WHERE DocumentID = @DocumentID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@DocumentID", documentID);

                // добавить проверку
                connection.Open();
                command.ExecuteNonQuery();
            }

            // удаление всех записей из M2M, если у документа был частный доступ
            if (access == DocumentAccess.Partial)
            {
                RemoveDocumentFromM2M(documentID);
            }
        }

        /// <summary>
        /// Закрыть пользователю доступ к документу
        /// </summary>
        public void DeletePartialAccessToUser(int userID, int documentID)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "DELETE FROM Documents " +
                    "WHERE DocumentID = @DocumentID AND UserID = @UserID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@DocumentID", documentID);
                command.Parameters.AddWithValue("@UserID", userID);

                // добавить проверку
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Поиск по заголовку среди всех документов
        /// </summary>
        public IEnumerable<Document> DocumentsSearchByTitle(string title)
        {
            IEnumerable<Tag> tags = null;
            return GetDocuments(tags, title);
        }

        /// <summary>
        /// Поиск по тегам среди всех документов
        /// </summary>
        public IEnumerable<Document> GetDocuments(IEnumerable<Tag> tags)
        {
            return GetDocuments(tags, string.Empty);
        }

        /// <summary>
        /// Возвращает документы (среди всех) по указанным тегам и названию
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public IEnumerable<Document> GetDocuments(IEnumerable<Tag> tags, string title)
        {
            var tagList = tags as List<Tag>;
            string tables = "[Documents] as D JOIN [Users] as U ON U.UserID = D.UserID " +
                            "JOIN[Tags] as T ON D.TagID = T.TagID ";
            string conditions = (!string.IsNullOrEmpty(title) ? "CHARINDEX('" + title + "', Title COLLATE Latin1_General_CI_AS) != 0 " : null);

            if (tagList != null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    conditions += " AND ";
                }

                for (var i = 0; i < tagList.Count(); i++)
                {
                    if (i == 0)
                    {
                        conditions += "(";
                    }
                    conditions += tagList[i].TagID + " = D.TagID ";

                    if (i != tagList.Count - 1)
                    {
                        conditions += " OR ";
                    }

                    if (i == tags.Count() - 1)
                    {
                        conditions += ")";
                    }
                }
            }

            return GetDocuments(tables, conditions);
        }

        /// <summary>
        /// Возвращает полную информацию документа по его id
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        public Document GetDocument(int docID)
        {
            var tables = " [Documents] as D JOIN[Users] as U " +
                         "ON U.UserID = D.UserID " +
                         "JOIN [Tags] as T ON D.TagID = T.TagID ";
            var conditions = " DocumentID = " + docID.ToString();

            var docs = GetDocuments(tables, conditions);

            var doc = new Document();
            if (docs != null)
            {
                doc = docs.First();
            }

            return doc;
        }

        /// <summary>
        /// Возвращает все документы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Document> GetDocuments()
        {
            string tables = " [Documents] as D JOIN [Users] as U ON U.UserID = D.UserID " +
                            "JOIN [Tags] as T ON D.TagID = T.TagID ";
            string condition = string.Empty;

            return GetDocuments(tables, condition);
        }

        /// <summary>
        /// Возвращает все документы пользователя с заданным id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<Document> GetUserDocuments(int userID)
        {
            var tables = " [Documents] as D JOIN [Users] as U ON U.UserID = D.UserID " +
                         "JOIN [Tags] as T ON D.TagID = T.TagID ";
            var conditions = " D.UserID = " + userID.ToString();

            return GetDocuments(tables, conditions);
        }

        /// <summary>
        /// Возвращает все документы, к которым пользователь имеет доступ
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<Document> GetUsersAvailableDocuments(int userID)
        {
            var tables = " [Documents] as D JOIN [M2M_Users_Documents] as M " +
                         "ON D.DocumentID = M.DocumentID" +
                         "JOIN [Tags] as T ON D.TagID = T.TagID " +
                         "JOIN [Users] as U ON U.UserID = M.UserID  ";
            var conditions = " M.UserID = " + userID.ToString();

            return GetDocuments(tables, conditions);
        }

        /// <summary>
        /// Хеширует строку
        /// </summary>
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

        /// <summary>
        /// Информация о теге по его id
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public Tag GetTag(int tagID)
        {
            var tag = new Tag();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TagID, TagName, Type FROM [Tags] WHERE TagID = @TagID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@TagID", tagID);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    tag.TagID = (int)reader["TagID"];
                    tag.TagName = reader["TagName"] as string;
                    tag.Type = reader["Type"] as string;
                }
            }

            return tag;
        }

        /// <summary>
        /// Возвращает id тега по его имени
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public int GetTagID(string tagName)
        {
            int id = 0;
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TagID FROM [Tags] WHERE TagName = @TagName";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@TagName", tagName);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = (int)reader["TagID"];
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Вернуть все теги
        /// </summary>
        public IEnumerable<Tag> GetTags()
        {
            var tags = new List<Tag>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TagID, TagName, Type FROM [Tags]";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tags.Add(new Tag());
                        tags[tags.Count - 1].TagID = (int)reader["TagID"];
                        tags[tags.Count - 1].TagName = reader["TagName"] as string;
                        tags[tags.Count - 1].Type = reader["Type"] as string;
                    }
                }
            }

            return tags;
        }

        //переделать
        /// <summary>
        /// Поиск документов, доступных пользователю.
        /// </summary>
        public IEnumerable<Document> SearchForAvailableDocuments(IEnumerable<Tag> tags, string title, int userID)
        {
            string tables = " [Documents] as D JOIN [M2M_Users_Documents] as M " +
                         "ON D.DocumentID = M.DocumentID" +
                         "JOIN [Tags] as T ON D.TagID = T.TagID " +
                         "JOIN [Users] as U ON U.UserID = M.UserID ";
            var cond = CreateConditionsForSearch(tags, title);
            string conditions = " M.UserID = " + userID.ToString() + (string.IsNullOrEmpty(cond) ? "" : " AND " + cond);

            return GetDocuments(tables, conditions);
        }

        //переделать
        /// <summary>
        /// Поиск документов, загруженных пользователем
        /// </summary>
        public IEnumerable<Document> SearchUsersDocuments(IEnumerable<Tag> tags, string title, int userID)
        {
            var tables = "[Documents] as D JOIN[Users] as U ON U.UserID = D.UserID " +
                            "JOIN[Tags] as T ON D.TagID = T.TagID ";
            var cond = CreateConditionsForSearch(tags, title);
            var conditions = " D.UserID = " + userID.ToString() + (string.IsNullOrEmpty(cond) ? "" : " AND " + cond);

            return GetDocuments(tables, conditions);
        }

        private void AddHashInDocuments(int documentID, string hash)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "UPDATE Documents SET Hash = @Hash " +
                                      "WHERE DocumentID = @DocumentID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Hash", hash);
                command.Parameters.AddWithValue("@DocumentID", documentID);

                // проверка
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void RemoveDocumentFromM2M(int documentID)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "DELETE FROM Documents " +
                    "WHERE DocumentID = @DocumentID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@DocumentID", documentID);

                // добавить проверку
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private IEnumerable<Document> GetDocuments(string tables, string condition)
        {
            var docs = new List<Document>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT D.UserID, DocumentID, Date, Doc, " +
                                      "Title, AccessID, D.TagID, Hash, Login, TagName, Type " +
                                      "FROM " + tables +
                                      (!string.IsNullOrEmpty(condition) ? " WHERE " + condition : "");
                command.CommandType = CommandType.Text;

                // добавить проверку
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        docs.Add(new Document());
                        docs[docs.Count - 1].UserID = (int)reader["UserID"];
                        docs[docs.Count - 1].DocumentID = (int)reader["DocumentID"];
                        docs[docs.Count - 1].Date = (DateTime)reader["Date"];
                        docs[docs.Count - 1].Doc = reader["Doc"] as byte[];
                        docs[docs.Count - 1].Title = reader["Title"] as string;
                        docs[docs.Count - 1].AccessID = (DocumentAccess)reader["AccessID"];
                        docs[docs.Count - 1].Tag = new Tag();
                        docs[docs.Count - 1].Tag.TagID = (int)reader["TagID"];
                        docs[docs.Count - 1].Tag.Type = reader["Type"] as string;
                        docs[docs.Count - 1].Tag.TagName = reader["TagName"] as string;
                        docs[docs.Count - 1].Hash = reader["Hash"] as string;
                        docs[docs.Count - 1].UserName = reader["Login"] as string;
                    }
                }
            }

            return docs;
        }

        private string CreateConditionsForSearch(IEnumerable<Tag> tags, string title)
        {
            var tagList = tags as List<Tag>;
            var conditions = (!string.IsNullOrEmpty(title) ? " CHARINDEX('" + title + "', Title COLLATE Latin1_General_CI_AS) != 0 " : null);

            if (tagList != null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    conditions += " AND ";
                }

                for (var i = 0; i < tagList.Count(); i++)
                {
                    if (i == 0)
                    {
                        conditions += " (";
                    }
                    conditions += tagList[i].TagID + " = D.TagID ";

                    if (i != tagList.Count - 1)
                    {
                        conditions += " OR ";
                    }

                    if (i == tags.Count() - 1)
                    {
                        conditions += ") ";
                    }
                }
            }

            return conditions;
        }
    }
}
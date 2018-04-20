namespace FileStorage.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using Interfaces;
    using Models;

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
            if (access == DocumentAccess.Partial)
            {
                RemoveDocumentFromM2M(documentID);
            }

            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = string.Format(
                                      "DELETE FROM Documents " +
                                      "WHERE DocumentID = {0}", 
                                      documentID);
                command.CommandType = CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();
            }
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
        /// Возвращает полную информацию документа по его хешу
        /// </summary>
        public Document GetDocument(string hash)
        {
            var tables = " [Documents] as D JOIN[Users] as U " +
                         "ON U.UserID = D.UserID " +
                         "JOIN [Tags] as T ON D.TagID = T.TagID ";
            var conditions = " Hash = '" + hash + "'";

            var docs = GetDocuments(tables, conditions);

            var doc = new Document();
            if (docs.Count() != 0)
            {
                doc = docs.First();
            }

            return doc;
        }

        /// <summary>
        /// Хеширует строку
        /// </summary>
        public string GetHashString(string s)
        {
            var hash = string.Format("{0:X}", s.GetHashCode());
            return hash;
        }

        /// <summary>
        /// Возвращает id владельца документа
        /// </summary>
        public int GetHolder(int docID)
        {
            int holderId = 0;

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = string.Format(
                                        "SELECT UserID FROM [Documents] WHERE DocumentID = {0} ",
                                        docID);
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        holderId = (int)reader["UserID"];
                    }
                }
            }

            return holderId;
        }

        /// <summary>
        /// Возвращает документы (среди всех) по указанным тегам и названию
        /// </summary>
        public IEnumerable<Document> SearchDocuments(IEnumerable<Tag> tags, string title)
        {
            var tagList = tags as List<Tag>;
            string tables = "[Documents] as D JOIN [Users] as U ON U.UserID = D.UserID " +
                            "JOIN[Tags] as T ON D.TagID = T.TagID ";
            var conditions = new StringBuilder(!string.IsNullOrEmpty(title) ?
                                string.Format("CHARINDEX('{0}', Title COLLATE Latin1_General_CI_AS) != 0 ", title) : null);

            if (tagList != null)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    conditions.AppendFormat("AND");
                }

                for (var i = 0; i < tagList.Count(); i++)
                {
                    if (i == 0)
                    {
                        conditions.AppendFormat("(");
                    }

                    conditions.AppendFormat("{0} = D.TagID ", tagList[i].TagID);

                    if (i != tagList.Count - 1)
                    {
                        conditions.AppendFormat(" OR ");
                    }

                    if (i == tags.Count() - 1)
                    {
                        conditions.AppendFormat(")");
                    }
                }
            }

            return GetDocuments(tables, conditions.ToString());
        }

        /// <summary>
        /// Поиск документов, доступных пользователю.
        /// </summary>
        public IEnumerable<Document> SearchAvailableDocuments(IEnumerable<Tag> tags, string title, int userID)
        {
            string tables = " [Documents] as D JOIN [M2M_Users_Documents] as M " +
                         " ON D.DocumentID = M.DocumentID " +
                         " JOIN [Tags] as T ON D.TagID = T.TagID " +
                         " JOIN [Users] as U ON U.UserID = M.UserID ";
            var cond = CreateConditionsForSearch(tags, title);
            string conditions = string.Format(" M.UserID =  {0} {1}", userID.ToString(), string.IsNullOrEmpty(cond) ? string.Empty : " AND " + cond);

            return GetDocuments(tables, conditions);
        }

        /// <summary>
        /// Поиск документов, загруженных пользователем
        /// </summary>
        public IEnumerable<Document> SearchUsersDocuments(IEnumerable<Tag> tags, string title, int userID)
        {
            var tables = "[Documents] as D JOIN[Users] as U ON U.UserID = D.UserID " +
                            "JOIN[Tags] as T ON D.TagID = T.TagID ";
            var cond = CreateConditionsForSearch(tags, title);
            var conditions = string.Format(
                " D.UserID = {0} {1}",
                userID.ToString(),
                string.IsNullOrEmpty(cond) ? string.Empty : " AND " + cond);

            return GetDocuments(tables, conditions);
        }

        /// <summary>
        /// Поиск среди документов с открытым доступом
        /// </summary>
        public IEnumerable<Document> SearchPublicDocuments(IEnumerable<Tag> tags, string title)
        {
            string tables = " [Documents] as D JOIN [Users] as U ON U.UserID = D.UserID " +
                            " JOIN [Tags] as T ON D.TagID = T.TagID ";
            var cond = CreateConditionsForSearch(tags, title);
            var conditions = string.Format(" D.AccessID = {0} {1}", (int)DocumentAccess.Public, string.IsNullOrEmpty(cond) ? string.Empty : " AND " + cond);

            return GetDocuments(tables, conditions);
        }

        private void AddHashInDocuments(int documentID, string hash)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = string.Format(
                                      "UPDATE [Documents] SET Hash = '{0}' " +
                                      "WHERE DocumentID = '{1}'", 
                                      hash, 
                                      documentID);
                command.CommandType = CommandType.Text;

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
                command.CommandText = string.Format(
                    "DELETE FROM [M2M_Users_Documents] WHERE DocumentID = {0}", 
                    documentID);
                command.CommandType = CommandType.Text;

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
                command.CommandText = string.Format(
                                      "SELECT D.UserID, D.DocumentID, Date, Doc, " +
                                      "Title, AccessID, D.TagID, Hash, Login, TagName, Type " +
                                      "FROM {0} {1}",
                                      tables,
                                      !string.IsNullOrEmpty(condition) ? " WHERE " + condition : string.Empty);
                command.CommandType = CommandType.Text;

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
            var conditions = new StringBuilder(!string.IsNullOrEmpty(title) ?
                string.Format(" CHARINDEX('{0}', Title COLLATE Latin1_General_CI_AS) != 0 ", title)
                : null);

            if (tagList != null && tagList.Count() != 0)
            {
                if (!string.IsNullOrEmpty(title))
                {
                    conditions.AppendFormat(" AND ");
                }

                for (var i = 0; i < tagList.Count(); i++)
                {
                    if (i == 0)
                    {
                        conditions.AppendFormat(" (");
                    }

                    conditions.AppendFormat("{0} = D.TagID ", tagList[i].TagID);

                    if (i != tagList.Count - 1)
                    {
                        conditions.AppendFormat(" OR ");
                    }

                    if (i == tags.Count() - 1)
                    {
                        conditions.AppendFormat(") ");
                    }
                }
            }

            return conditions.ToString();
        }
    }
}
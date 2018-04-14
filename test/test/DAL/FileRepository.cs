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
    public class FileRepository : IFileRepository
    {
        // DI?
        private DbProviderFactory factory;
        private string connectionString;

        public FileRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings["smthg"];
            this.connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

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

        public void ChangeAccess(int documentID, DocumentAccess newAccess, DocumentAccess oldAccess)
        {
            if(oldAccess == DocumentAccess.Partial && newAccess != DocumentAccess.Partial)
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
                command.Parameters.AddWithValue("@TagID", doc.TagID);

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

                AddHashInDocuments( Convert.ToInt32(docId), hash);
            }
        }

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

        public IEnumerable<Document> DocumentsSearchByTitle(string title)
        {
            var docs = new List<Document>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT D.UserID, DocumentID, Date, Doc, " +
                                      "Title, AccessID, TagID, Hash, Login " +
                                      "FROM [Documents] as D JOIN [Users] as U " +
                                      "ON U.UserID = D.UserID" +
                                      "WHERE CHARINDEX(@Title, Title COLLATE Latin1_General_CI_AS) != 0";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Title", title);

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
                        docs[docs.Count - 1].TagID = (int)reader["TagID"];
                        docs[docs.Count - 1].Hash = reader["Hash"] as string;
                        docs[docs.Count - 1].UserName = reader["Login"] as string;
                    }
                }
            }

            return docs;
            throw new NotImplementedException();
        }

        public IEnumerable<Document> GetDocuments(IEnumerable<Tag> tags)
        {
            var docs = new List<Document>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                foreach(var tag in tags)
                {
                    command.CommandText = "SELECT D.UserID, DocumentID, Date, Doc, " +
                                      "Title, AccessID, TagID, Hash, Login " +
                                      "FROM [Documents] as D JOIN [Users] as U " +
                                      "ON U.UserID = D.UserID" +
                                      "WHERE TagID = @TagID";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@TagID", tag.TagID);

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
                            docs[docs.Count - 1].TagID = (int)reader["TagID"];
                            docs[docs.Count - 1].Hash = reader["Hash"] as string;
                            docs[docs.Count - 1].UserName = reader["Login"] as string;
                        }
                    }
                }
            }

            return docs;
        }

        public IEnumerable<Document> GetDocuments(IEnumerable<Tag> tags, string title)
        {
            var docs = new List<Document>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                foreach (var tag in tags)
                {
                    command.CommandText = "SELECT D.UserID, DocumentID, Date, Doc, " +
                                      "Title, AccessID, TagID, Hash, Login " +
                                      "FROM [Documents] as D JOIN [Users] as U " +
                                      "ON U.UserID = D.UserID WHERE TagID = @TagID AND " +
                                      "CHARINDEX(@Title, Title COLLATE Latin1_General_CI_AS) != 0";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@TagID", tag.TagID);
                    command.Parameters.AddWithValue("@Title", title);

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
                            docs[docs.Count - 1].TagID = (int)reader["TagID"];
                            docs[docs.Count - 1].Hash = reader["Hash"] as string;
                            docs[docs.Count - 1].UserName = reader["Login"] as string;
                        }
                    }
                }
            }

            return docs;
        }

        public Document GetDocument(int docID)
        {
            var doc = new Document();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT D.UserID, DocumentID, Date, Doc, " +
                                      "Title, AccessID, TagID, Hash, Login " +
                                      "FROM [Documents] as D JOIN [Users] as U " +
                                      "ON U.UserID = D.UserID " +
                                      "WHERE DocumentID = @DocID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@DocID", docID);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                        doc.UserID = (int)reader["UserID"];
                        doc.DocumentID = (int)reader["DocumentID"];
                        doc.Date = (DateTime)reader["Date"];
                        doc.Doc = reader["Doc"] as byte[];
                        doc.Title = reader["Title"] as string;
                        doc.AccessID = (DocumentAccess)reader["AccessID"];
                        doc.TagID = (int)reader["TagID"];
                        doc.Hash = reader["Hash"] as string;
                        doc.UserName = reader["Login"] as string;
                }
            }

            return doc;
        }

        public IEnumerable<Document> GetDocuments()
        {
            var docs = new List<Document>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT D.UserID, DocumentID, Date, Doc, " +
                                      "Title, AccessID, TagID, Hash, Login " +
                                      "FROM [Documents] as D JOIN [Users] as U " +
                                      "ON U.UserID = D.UserID";
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
                        docs[docs.Count - 1].TagID = (int)reader["TagID"];
                        docs[docs.Count - 1].Hash = reader["Hash"] as string;
                        docs[docs.Count - 1].UserName = reader["Login"] as string;
                    }
                }
            }

            return docs;
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

        public Tag GetTag(int tagID)
        {
            var tag = new Tag();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TagID, TagName FROM [Tags] WHERE TagID = @TagID";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@TagID", tagID);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    tag.TagID = (int)reader["TagID"];
                    tag.TagName = reader["TagName"] as string;
                }
            }

            return tag;
        }

        public IEnumerable<Tag> GetTags()
        {
            var tags = new List<Tag>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TagID, TagName FROM [Tags]";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tags.Add(new Tag());
                        tags[tags.Count - 1].TagID = (int)reader["TagID"];
                        tags[tags.Count - 1].TagName = reader["TagName"] as string;
                    }
                }
            }

            return tags;
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
    }
}
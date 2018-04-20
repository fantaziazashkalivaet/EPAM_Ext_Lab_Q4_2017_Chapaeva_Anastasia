namespace FileStorage.DAL
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using Interfaces;
    using Models;

    public class TagRepository : ITagRepository
    {
        private DbProviderFactory factory;
        private string connectionString;

        public TagRepository()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings["FilesDatabase"];
            this.connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        /// <summary>
        /// Информация о теге по его id
        /// </summary>
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
                    if (reader.Read())
                    {
                        tag.TagID = (int)reader["TagID"];
                        tag.TagName = reader["TagName"] as string;
                        tag.Type = reader["Type"] as string;
                    }
                }
            }

            return tag;
        }

        /// <summary>
        /// Возвращает id тега по его имени
        /// </summary>
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
                        tags.Add(new Tag()
                        {
                            TagID = (int)reader["TagID"],
                            TagName = reader["TagName"] as string,
                            Type = reader["Type"] as string
                    });
                    }
                }
            }

            return tags;
        }
    }
}
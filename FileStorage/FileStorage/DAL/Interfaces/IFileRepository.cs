namespace FileStorage.DAL.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IFileRepository
    {
        void CreateDocument(Document doc);

        void DeleteDocument(int documentID, DocumentAccess access);

        Document GetDocument(int docID);

        Document GetDocument(string hash);

        string GetHashString(string s);

        int GetHolder(int docID);

        IEnumerable<Document> SearchAvailableDocuments(IEnumerable<Tag> tags, string title, int userID);

        IEnumerable<Document> SearchDocuments(IEnumerable<Tag> tags, string title);

        IEnumerable<Document> SearchPublicDocuments(IEnumerable<Tag> tags, string title);

        IEnumerable<Document> SearchUsersDocuments(IEnumerable<Tag> tags, string title, int userID);
    }
}

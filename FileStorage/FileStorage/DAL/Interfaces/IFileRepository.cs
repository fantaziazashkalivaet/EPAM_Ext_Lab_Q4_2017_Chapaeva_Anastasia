using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    public interface IFileRepository
    {
        IEnumerable<Document> GetDocuments();

        Document GetDocument(int docID);

        IEnumerable<Document> GetDocuments(IEnumerable<Tag> tags);

        IEnumerable<Document> GetDocuments(IEnumerable<Tag> tags, string title);

        IEnumerable<Document> GetUserDocuments(int userID);

        IEnumerable<Document> GetUsersAvailableDocuments(int userID);

        IEnumerable<Document> DocumentsSearchByTitle(string title);

        void CreateDocument(Document doc);

        void DeleteDocument(int documentID, DocumentAccess access);

        void ChangeAccess(int documentID, DocumentAccess newAccess, DocumentAccess oldAccess);

        void AddPartialAccessToUser(int userID, int documentID);

        void DeletePartialAccessToUser(int userID, int documentID);

        IEnumerable<Tag> GetTags();

        Tag GetTag(int tagID);

        int GetTagID(string tagName);

        string GetHashString(string s);

        IEnumerable<Document> SearchForAvailableDocuments(IEnumerable<Tag> tags, string title, int userID);

        IEnumerable<Document> SearchUsersDocuments(IEnumerable<Tag> tags, string title, int userID);
    }
}

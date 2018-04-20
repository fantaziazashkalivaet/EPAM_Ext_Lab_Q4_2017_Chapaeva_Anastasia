namespace FileStorage.DAL.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IAccessRepository
    {
        bool CheckUserAccess(int userID, int docID);

        void ChangeAccess(int documentID, DocumentAccess newAccess, DocumentAccess oldAccess);

        void ChangePartialAccessToUser(int userID, int documentID);

        IEnumerable<DocumentAccess> GetAccess();

        IEnumerable<User> UsersWithAccess(int docID);
    }
}

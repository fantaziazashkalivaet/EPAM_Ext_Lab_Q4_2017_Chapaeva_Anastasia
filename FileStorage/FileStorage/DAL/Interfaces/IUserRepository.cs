namespace FileStorage.DAL.Interfaces
{
    using Models;

    public interface IUserRepository
    {
        bool CheckLogin(string login, string passwordHash);

        bool CheckUser(string login);

        int CreateUser(User user);

        string GetHashString(string s);

        User SearchUserByID(int id);

        User SearchUserByLogin(string login);
    }
}

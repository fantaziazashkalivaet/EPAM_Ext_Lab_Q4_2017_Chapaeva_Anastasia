using FileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DAL.Interfaces
{
    public interface IUserRepository
    {
        bool CheckUser(User user);

        int CreateUser(User user);

        string GetHashString(string s);

        User SearchUserByID(int id);

        User SearchUserByLogin(string login);
    }
}

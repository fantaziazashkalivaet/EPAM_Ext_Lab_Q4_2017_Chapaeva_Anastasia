using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.DAL.Models;

namespace test.DAL
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

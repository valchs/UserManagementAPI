using System.Collections.Generic;
using UserManagementLibrary.Models;

namespace UserManagementLibrary.DataAccess
{
    public interface IUserData
    {
        void DeleteUser(int id);
        List<User> GetUsers();
        List<User> GetUsers(int id = 0, string email = null);
        int SaveUser(UserInput user, int? id = null);
    }
}
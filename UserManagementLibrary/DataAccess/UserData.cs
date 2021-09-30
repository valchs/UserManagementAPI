using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserManagementLibrary.DataAccess.Internal;
using UserManagementLibrary.Models;

namespace UserManagementLibrary.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;
        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public List<User> GetUsers() => GetUsers(0, null);
        public List<User> GetUsers(int id = 0, string email = null)
        {
            var p = new DynamicParameters();
            p.Add("@nId", id);
            p.Add("@cEmail", email);
            return _db.LoadData<User, dynamic>("dbo.get_Users", p, "UserConnection");
        }

        public int SaveUser(UserInput user, int? id = null)
        {
            var p = new DynamicParameters();
            p.Add("@nId", id);
            p.Add("@cFirstName", user.FirstName);
            p.Add("@cLastName", user.LastName);
            p.Add("@cPhoneNumber", user.PhoneNumber);
            p.Add("@cEmail", user.Email);
            p.Add("@nUserId", 0, DbType.Int32, direction: ParameterDirection.Output);
            _db.SaveData("dbo.set_Users", p, "UserConnection");
            return p.Get<int>("nUserId");
        }

        public void DeleteUser(int id)
        {
            var p = new DynamicParameters();
            p.Add("@nId", id);
            p.Add("@bDel", true);
            _db.SaveData("dbo.set_Users", p, "UserConnection");
        }
    }
}

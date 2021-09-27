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
    public class UserData
    {
        private readonly IConfiguration _config;
        public UserData(IConfiguration config)
        {
            _config = config;
        }

        public List<User> GetUsers(int id = 0, string email = null)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new DynamicParameters();
            p.Add("@nId", id);
            p.Add("@cEmail", email);
            return sql.LoadData<User, dynamic>("dbo.get_Users", p, "UserConnection");
        }

        public int SaveUser(UserInput user, int? id = null)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new DynamicParameters();
            p.Add("@nId", id);
            p.Add("@cFirstName", user.FirstName);
            p.Add("@cLastName", user.LastName);
            p.Add("@cPhoneNumber", user.PhoneNumber);
            p.Add("@cEmail", user.Email);
            p.Add("@nUserId", 0, DbType.Int32, direction: ParameterDirection.Output);
            sql.SaveData("dbo.set_Users", p, "UserConnection");
            return p.Get<int>("nUserId");
        }

        public void DeleteUser(int id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new DynamicParameters();
            p.Add("@nId", id);
            p.Add("@bDel", true);
            sql.SaveData("dbo.set_Users", p, "UserConnection");
        }
    }
}

using ApplicationCore.DomainServices;
using System;
using System.Linq;
using System.Collections.Generic;
using ApplicationCore.DomainModel;
using System.Collections;

using NI.Data;

namespace Infrastructure.DomainServices
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public OperationResult CreateUser(string username, string passwordHash, int roleId)
        {
            var fields = new Hashtable() {
                    { "Name", username },
                    { "Password", passwordHash },
                    { "RoleID", roleId }
            };
            try
            {
                db.Insert(tableName: "Users",
                          data: fields);

                return new OperationResult { Succeded = true };
            }
            catch(Exception e)
            {
                return new OperationResult { Succeded = false };
            }
        }
        public User GetUserByID(int id)
        {
            var query = new Query(tableName: "Users",
                                  condition: (QField)"ID" == new QConst(id));

            query.Fields = new[] {
                    (QField)"ID",
                    (QField)"Name",
                    (QField)"Password",
                    (QField)"RoleID"
            };

            IDictionary dict = db.LoadAllRecords(query)[0];

            User user = new User
            {
                UserId = Convert.ToInt32(dict["ID"]),
                Name = dict["Name"].ToString(),
                Password = dict["Password"].ToString(),
                RoleId = Convert.ToInt32(dict["RoleID"])
            };

            return user;
        }
        public User GetUser(string username)
        {
            var query = new Query(tableName: "Users",
                                  condition: (QField)"Name" == new QConst(username));

            query.Fields = new[] {
                    (QField)"ID",
                    (QField)"Name",
                    (QField)"Password",
                    (QField)"RoleID"
            };
            var records = db.LoadAllRecords(query);
            if (records != null)
            {
                IDictionary dict = records[0];

                var user = new User
                {
                    UserId = Convert.ToInt32(dict["ID"]),
                    Name = dict["Name"].ToString(),
                    Password = dict["Password"].ToString(),
                    RoleId = Convert.ToInt32(dict["RoleID"])
                };

                return user;
            }
            return null;
        }
        public Role GetRoleById(int roleId)
        {
            var query = new Query(tableName: "Roles",
                                  condition: (QField)"ID" == new QConst(roleId));

            query.Fields = new[] {
                    (QField)"ID",
                    (QField)"Name",
            };

            IDictionary dict = db.LoadAllRecords(query)[0];

            var role = new Role
            {
                RoleId = Convert.ToInt32(dict["ID"]),
                Name = dict["Name"].ToString()
            };

            return role;
        }
    }
}

using ApplicationCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainServices
{
    public interface IUserRepository : IDisposable
    {
        OperationResult CreateUser(string username, string passwordHash, int roleId);
        User GetUserByID(int id);
        User GetUser(string username);
        Role GetRoleById(int roleId);
   
    }
}


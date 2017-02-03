using ApplicationCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ApplicationServices
{
    public interface IAccountService
    {
        bool CreateUser(string username, string password);
        User GetUser(string username, string password);
        int GetCurrentUserId();
        void Logout();
        void Login(int userId, string username, string role);
    }
}

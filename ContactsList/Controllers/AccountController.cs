using ApplicationCore.ApplicationServices;
using ApplicationCore.DomainModel;
using ApplicationCore.DomainServices;
using ContactsList.Models;
using Infrastructure.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactsList.Controllers
{
    public class AccountController : Controller
    {
        IAccountService accountService;
        IUserRepository userRepository;
        public AccountController(IAccountService accountService, IUserRepository userRepository)
        {
            this.accountService = accountService;
            this.userRepository = userRepository;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(UserViewmodel model)
        {
            User user = accountService.GetUser(model.UserName, model.Password);
            if (user != null)
            {
                Role role = null;
                using (var db = userRepository)
                {
                   role = userRepository.GetRoleById(user.RoleId);
                }
               
                accountService.Login(user.UserId, user.Name, role.Name);
                if (role.Name == "Admin")
                    //return RedirectToRoute("Admin");
                    Response.Redirect("~/admin/companies.aspx");

                else
                    return RedirectToAction("Index", "Default");
            }
            return new HttpUnauthorizedResult();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            accountService.Logout();
            return RedirectToAction("Index", "Default");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Register(UserViewmodel model)
        {
            accountService.CreateUser(model.UserName, model.Password);
            return RedirectToAction("Login");
        }
    }
}
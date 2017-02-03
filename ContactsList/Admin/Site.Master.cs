using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactsList.Admin
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAuth = HttpContext.Current.User.Identity.IsAuthenticated;
            bool isAdmin = HttpContext.Current.User.IsInRole("Admin");

            if (!isAuth && !isAdmin)
                Response.RedirectToRoute("Default");
            
        }
    }
}
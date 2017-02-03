using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(ContactsList.Startup))]
namespace ContactsList
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

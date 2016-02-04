using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LukeMVC.Startup))]
namespace LukeMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

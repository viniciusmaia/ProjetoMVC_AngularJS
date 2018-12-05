using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViniciusMaiaITIXWebApp.Startup))]
namespace ViniciusMaiaITIXWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

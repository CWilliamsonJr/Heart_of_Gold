using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hearts_of_Gold.Startup))]
namespace Hearts_of_Gold
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

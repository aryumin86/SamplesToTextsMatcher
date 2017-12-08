using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebArm.Startup))]
namespace WebArm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

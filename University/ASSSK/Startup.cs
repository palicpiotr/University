using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASSSK.Startup))]
namespace ASSSK
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

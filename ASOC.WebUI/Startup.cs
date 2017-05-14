using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASOC.WebUI.Startup))]
namespace ASOC.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IP.VD.Solution.UI.Startup))]
namespace IP.VD.Solution.UI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

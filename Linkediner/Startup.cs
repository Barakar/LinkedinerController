using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Linkediner.Startup))]

namespace Linkediner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

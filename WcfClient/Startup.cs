using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WcfClient.Startup))]
namespace WcfClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

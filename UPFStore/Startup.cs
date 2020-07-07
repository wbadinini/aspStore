using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UPFStore.Startup))]
namespace UPFStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

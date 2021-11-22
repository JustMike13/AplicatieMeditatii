using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplicatieMeditatii.Startup))]
namespace AplicatieMeditatii
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

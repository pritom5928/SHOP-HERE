using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopHere.Web.Startup))]
namespace ShopHere.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

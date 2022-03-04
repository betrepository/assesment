using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BET_ecommerce_website.Startup))]
namespace BET_ecommerce_website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

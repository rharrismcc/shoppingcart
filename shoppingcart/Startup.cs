using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(shoppingcart.Startup))]
namespace shoppingcart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

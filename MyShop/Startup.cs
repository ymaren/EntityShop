using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShop.Startup))]
namespace MyShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

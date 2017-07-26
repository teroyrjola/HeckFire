using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Calculator.Startup))]
namespace Calculator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

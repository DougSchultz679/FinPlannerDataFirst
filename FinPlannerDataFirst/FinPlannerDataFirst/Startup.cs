using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinPlannerDataFirst.Startup))]
namespace FinPlannerDataFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

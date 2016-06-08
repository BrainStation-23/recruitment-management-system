using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RecruitmentManagementSystem.App.Startup))]

namespace RecruitmentManagementSystem.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

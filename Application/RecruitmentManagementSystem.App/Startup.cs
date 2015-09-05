using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecruitmentManagementSystem.App.Startup))]

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
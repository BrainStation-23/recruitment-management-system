using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using RecruitmentManagementSystem.App.Infrastructure.Tasks;
using System.Web;

namespace RecruitmentManagementSystem.App
{
    public class MvcApplication : HttpApplication
    {
        public IUnityContainer UnityContainer;

        public MvcApplication()
        {
            UnityContainer = UnityConfig.GetConfiguredContainer();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            foreach (var task in UnityContainer.ResolveAll<IRunAtInit>())
            {
                task.Execute();
            }

            foreach (var task in UnityContainer.ResolveAll<IRunAtStartup>())
            {
                task.Execute();
            }
        }

        public void Application_BeginRequest()
        {
            if (UnityContainer == null)
            {
                UnityContainer = UnityConfig.GetConfiguredContainer();
            }

            foreach (var task in UnityContainer.ResolveAll<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in UnityContainer.ResolveAll<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in
                    UnityContainer.ResolveAll<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                UnityContainer.Dispose();
                UnityContainer = null;
            }
        }
    }
}
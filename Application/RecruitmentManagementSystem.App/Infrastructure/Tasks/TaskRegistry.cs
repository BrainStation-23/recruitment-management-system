using System.Linq;
using Microsoft.Practices.Unity;

namespace RecruitmentManagementSystem.App.Infrastructure.Tasks
{
    public class TaskRegistry
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies().
                    Where(
                        type =>
                            typeof (IRunAtInit).IsAssignableFrom(type) ||
                            typeof (IRunAtStartup).IsAssignableFrom(type) ||
                            typeof (IRunOnError).IsAssignableFrom(type) ||
                            typeof (IRunOnEachRequest).IsAssignableFrom(type) ||
                            typeof (IRunAfterEachRequest).IsAssignableFrom(type) ||
                            typeof (IRunBeforeEachRequest).IsAssignableFrom(type)),
                WithMappings.FromAllInterfaces,
                WithName.TypeName);
        }
    }
}
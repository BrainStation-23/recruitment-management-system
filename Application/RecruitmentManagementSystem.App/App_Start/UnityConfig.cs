using System;
using System.Linq;
using Microsoft.Practices.Unity;
using RecruitmentManagementSystem.App.Controllers;
using RecruitmentManagementSystem.Core.Tasks;

namespace RecruitmentManagementSystem.App
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default
                );

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
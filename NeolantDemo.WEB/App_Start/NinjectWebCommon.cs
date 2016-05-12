using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NeolantDemo.BLL.DTO;
using NeolantDemo.BLL.Infrastructure;
using NeolantDemo.BLL.Services;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.WEB;
using NeolantDemo.WEB.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace NeolantDemo.WEB
{
    /// <summary>
    /// Bootstrapper for the application.
    /// </summary>
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Get additional NinjectModules
        /// </summary>
        private static IEnumerable<INinjectModule> Modules
        {
            get
            {
                const string connectionStringName = @"neolant";
                string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

                return new INinjectModule[]
                {
                    new ServiceModule(connectionString)
                };
            }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Load(Modules);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            kernel.Bind<IDisposableRepositoryWithHierarchy<FacilityDTO>>().To<FacilityService>();
            kernel.Bind<IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO>>()
                .To<FacilityWithPropertiesService>();
            kernel.Bind<IDisposableRepository<PropertyKindDTO>>().To<PropertyKindService>();
            kernel.Bind<IDisposableRepository<FacilityClassDTO>>().To<FacilityClassService>();
            kernel.Bind<IDisposableRepository<CommonUniversalPropertyDTO>>().To<CommonUniversalPropertyService>();
        }
    }
}
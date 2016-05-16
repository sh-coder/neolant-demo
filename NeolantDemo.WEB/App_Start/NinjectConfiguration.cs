using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NeolantDemo.BLL.Infrastructure;
using NeolantDemo.WEB;
using NeolantDemo.WEB.Utils;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectConfiguration), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectConfiguration), "Stop")]

namespace NeolantDemo.WEB
{
    /// <summary>
    /// Bootstrapper for the application.
    /// </summary>
    public static class NinjectConfiguration
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Get additional NinjectModules
        /// </summary>
        public static INinjectModule[] Modules
        {
            get
            {
                const string connectionStringName = @"neolant";
                string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

                return new INinjectModule[]
                {
                    new ServiceModule(connectionString),
                    new NinjectBindingsModule()
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
            var kernel = new StandardKernel(Modules);

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }
    }
}
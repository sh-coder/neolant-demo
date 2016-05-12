using NeolantDemo.DAL.Interfaces;
using NeolantDemo.DAL.Repositories;
using Ninject.Modules;

namespace NeolantDemo.BLL.Infrastructure
{
    /// <summary>
    /// A loadable unit that defines custom bindings.
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connection">Connection string.</param>
        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}
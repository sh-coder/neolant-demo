using System.Web.Http.Dependencies;
using Ninject;

namespace NeolantDemo.WEB.Utils
{
    /// <summary>
    /// Represents a dependency injection container.
    /// </summary>
    /// <seealso cref="NeolantDemo.WEB.Utils.NinjectDependencyScope" />
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyResolver" /> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}
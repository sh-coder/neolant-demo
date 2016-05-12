using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace NeolantDemo.WEB.Utils
{
    /// <summary>
    /// Provides a Ninject implementation of IDependencyScope
    /// which resolves services using the Ninject container.
    /// </summary>
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyScope" />
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyScope" /> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>
        /// The retrieved service.
        /// </returns>
        /// <exception cref="ObjectDisposedException">this;This scope has been disposed</exception>
        public object GetService(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return _resolver.TryGet(serviceType);
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>
        /// The retrieved collection of services.
        /// </returns>
        /// <exception cref="ObjectDisposedException">this;This scope has been disposed</exception>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return _resolver.GetAll(serviceType);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var disposable = _resolver as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
                _resolver = null;
            }
        }
    }
}
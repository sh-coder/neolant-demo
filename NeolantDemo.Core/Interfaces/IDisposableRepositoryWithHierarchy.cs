using System;

namespace NeolantDemo.Core.Interfaces
{
    /// <summary>
    /// Interface IDisposableRepositoryWithHierarchy
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="NeolantDemo.Core.Interfaces.IRepositoryWithHierarchy{T}" />
    /// <seealso cref="System.IDisposable" />
    public interface IDisposableRepositoryWithHierarchy<T> : IRepositoryWithHierarchy<T>, IDisposable
    {
    }
}
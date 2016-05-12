using System;

namespace NeolantDemo.Core.Interfaces
{
    /// <summary>
    /// Interface IDisposableRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="NeolantDemo.Core.Interfaces.IRepository{T}" />
    /// <seealso cref="System.IDisposable" />
    public interface IDisposableRepository<T> : IRepository<T>, IDisposable
    {
    }
}
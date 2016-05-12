using System.Collections.Generic;

namespace NeolantDemo.Core.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса получения иерархии сущности.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IRepositoryWithHierarchy<T> : IRepository<T>
    {
        /// <summary>
        /// Получить иерархию потомков сущности.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        /// <returns></returns>
        T GetHierarchy(long id);

        /// <summary>
        /// Получить иерархию потомков сущности в виде списка.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        /// <returns></returns>
        IEnumerable<T> GetHierarchyFlatten(long id);
    }
}
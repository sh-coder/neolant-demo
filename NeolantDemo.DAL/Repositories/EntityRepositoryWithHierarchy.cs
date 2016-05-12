using System.Collections.Generic;
using System.Linq;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.MSSQL;

namespace NeolantDemo.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для иерархичных данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityRepositoryWithHierarchy<T> : EntityRepository<T>, IRepositoryWithHierarchy<T>
        where T : BaseHierarchicalEntity, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepositoryWithHierarchy{T}" /> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public EntityRepositoryWithHierarchy(MSSQLCustomContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Получает данные из репозитория с заданным идентификатором.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <returns></returns>
        public T GetHierarchy(long instanceS)
        {
            IEnumerable<T> result = GetHierarchyFlatten(instanceS);
            if (result == null)
            {
                return null;
            }
            Dictionary<long, T> dict = result.ToDictionary(key => key.InstanceS);
            if (!dict.Any()) return null;

            foreach (T item in dict.Values)
            {
                T parent;
                if (item.ParentInstanceS == null || !dict.TryGetValue(item.ParentInstanceS.Value, out parent)) continue;
                if (parent.Children == null)
                {
                    parent.Children = new List<BaseHierarchicalEntity>(new List<T>());
                }
                parent.Children.Add(item);
            }
            return dict[instanceS];
        }

        /// <summary>
        /// Получает данные из репозитория с заданным идентификатором.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <returns></returns>
        /// <value>Идентификатор сущности.</value>
        public IEnumerable<T> GetHierarchyFlatten(long instanceS)
        {
            IEnumerable<T> result = DatabaseContext.SelectTableHierarchy<T>(instanceS);
            return result;
        }
    }
}
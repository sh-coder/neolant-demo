using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NeolantDemo.Core.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория данных типа <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Тип данных репозитория.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Получает все данные репозитория.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Получает данные из репозитория с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns></returns>
        T Get(long id);

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение фильтрации данных.</param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Создает данные в репозитории.
        /// </summary>
        /// <param name="item">Экземпляр данных.</param>
        /// <returns></returns>
        T Create(T item);

        /// <summary>
        /// Удаляет данные из репозитория с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        void Delete(long id);

        /// <summary>
        /// Удаляет все данные из репозитория.
        /// </summary>
        void DeleteAll();
    }
}
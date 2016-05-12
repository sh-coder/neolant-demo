using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.MSSQL;

namespace NeolantDemo.DAL.Repositories
{
    /// <summary>
    /// Базовый репозиторий данных.
    /// </summary>
    /// <typeparam name="T">Тип данных репозитория.</typeparam>
    /// <seealso cref="IRepository{T}" />
    public class EntityRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Контекст доступа к базе данных.
        /// </summary>
        protected readonly MSSQLCustomContext DatabaseContext;

        /// <summary>
        /// Initializes a new instance of the<see cref="EntityRepository{T}" />class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public EntityRepository(MSSQLCustomContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        /// <summary>
        /// Получает все данные репозитория.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get()
        {
            return DatabaseContext.SelectTableValues<T>();
        }

        /// <summary>
        /// Получает данные из репозитория с заданным идентификатором.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <returns></returns>
        public T Get(long instanceS)
        {
            return DatabaseContext.SelectTableValues<T>(instanceS).FirstOrDefault();
        }

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение получения данных.</param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            // TODO: Временный костыль для фильтрации данных из таблицы Property. Работает следующим образом:
            // TODO: Если в параметре есть IEnumerable<long> - то данное перечисление
            // TODO: используется как фильтр для колонки TARGET_CLASS_S.
            if (typeof (T) == typeof (Property))
            {
                var call = expression.Body as MethodCallExpression;
                if (call == null)
                {
                    throw new ArgumentException("Not a method call");
                }
                foreach (Expression argument in call.Arguments)
                {
                    LambdaExpression lambda = Expression.Lambda(argument, expression.Parameters);
                    Delegate d = lambda.Compile();
                    var value = d.DynamicInvoke(new object[1]) as IEnumerable<long>;
                    if (value != null)
                    {
                        return DatabaseContext.FindTableValues<Property>(value) as IEnumerable<T>;
                    }
                }
            }
            return Get().AsQueryable().Where(expression);
        }

        /// <summary>
        /// Создает данные в репозитории.
        /// </summary>
        /// <param name="item">Экземпляр данных.</param>
        /// <returns></returns>
        public T Create(T item)
        {
            DatabaseContext.InserTableValue<T>(item);
            return item;
        }

        /// <summary>
        /// Удаляет данные из репозитория с заданным идентификатором.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        public void Delete(long instanceS)
        {
            DatabaseContext.DeleteTableValue<T>(instanceS);
        }

        /// <summary>
        /// Удаляет все данные из репозитория.
        /// </summary>
        public void DeleteAll()
        {
            DatabaseContext.DeleteTableValue<T>();
        }
    }
}
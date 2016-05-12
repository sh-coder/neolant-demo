using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using EmitMapper;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.Interfaces;

namespace NeolantDemo.BLL.Services
{
    /// <summary>
    /// Сервис CRUD операции для видов свойств/атрибутов.
    /// </summary>
    public sealed class PropertyKindService : IDisposableRepository<PropertyKindDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="unitOfWork">Сервис для работы с сущностью.</param>
        public PropertyKindService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PropertyKindDTO> Get()
        {
            IEnumerable<PropertyKind> result = Database.PropertyKindRepository.Get();
            if (result == null)
            {
                return null;
            }

            ObjectsMapper<PropertyKind, PropertyKindDTO> mapper = ObjectMapperManager
                .DefaultInstance
                .GetMapper<PropertyKind, PropertyKindDTO>();

            var mappedResult = new Collection<PropertyKindDTO>();
            foreach (PropertyKind propertyKind in result)
            {
                mappedResult.Add(mapper.Map(propertyKind));
            }

            return mappedResult;
        }

        /// <summary>
        /// Получить объекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns></returns>
        public PropertyKindDTO Get(long id)
        {
            PropertyKind result = Database.PropertyKindRepository.Get(id);
            if (result == null)
            {
                return null;
            }

            PropertyKindDTO mappedResult = ObjectMapperManager
                .DefaultInstance
                .GetMapper<PropertyKind, PropertyKindDTO>()
                .Map(result);

            return mappedResult;
        }

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение фильтрации данных.</param>
        /// <returns></returns>
        public IEnumerable<PropertyKindDTO> Find(Expression<Func<PropertyKindDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать новый объект.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого объекта.</param>
        /// <returns></returns>
        public PropertyKindDTO Create(PropertyKindDTO item)
        {
            PropertyKind result = ObjectMapperManager
                .DefaultInstance
                .GetMapper<PropertyKindDTO, PropertyKind>()
                .Map(item);

            Database.PropertyKindRepository.Create(result);

            return item;
        }

        /// <summary>
        /// Удалить объект по  идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public void Delete(long id)
        {
            Database.PropertyKindRepository.Delete(id);
        }

        /// <summary>
        /// Удалить все объекты.
        /// </summary>
        public void DeleteAll()
        {
            Database.PropertyKindRepository.DeleteAll();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
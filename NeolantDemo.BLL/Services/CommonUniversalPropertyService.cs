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
    public sealed class CommonUniversalPropertyService : IDisposableRepository<CommonUniversalPropertyDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="unitOfWork">Сервис для работы с сущностью.</param>
        public CommonUniversalPropertyService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommonUniversalPropertyDTO> Get()
        {
            IEnumerable<CommonUniversalProperty> result = Database.CommonUniversalPropertyRepository.Get();
            if (result == null)
            {
                return null;
            }

            ObjectsMapper<CommonUniversalProperty, CommonUniversalPropertyDTO> mapper = ObjectMapperManager
                .DefaultInstance
                .GetMapper<CommonUniversalProperty, CommonUniversalPropertyDTO>();

            var mappedResult = new Collection<CommonUniversalPropertyDTO>();
            foreach (CommonUniversalProperty commonUniversalProperty in result)
            {
                mappedResult.Add(mapper.Map(commonUniversalProperty));
            }

            return mappedResult;
        }

        /// <summary>
        /// Получить объекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns></returns>
        public CommonUniversalPropertyDTO Get(long id)
        {
            CommonUniversalProperty result = Database.CommonUniversalPropertyRepository.Get(id);
            if (result == null)
            {
                return null;
            }

            CommonUniversalPropertyDTO mappedResult = ObjectMapperManager
                .DefaultInstance
                .GetMapper<CommonUniversalProperty, CommonUniversalPropertyDTO>()
                .Map(result);

            return mappedResult;
        }

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение фильтрации данных.</param>
        /// <returns></returns>
        public IEnumerable<CommonUniversalPropertyDTO> Find(
            Expression<Func<CommonUniversalPropertyDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать новый объект.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого объекта.</param>
        /// <returns></returns>
        public CommonUniversalPropertyDTO Create(CommonUniversalPropertyDTO item)
        {
            CommonUniversalProperty result = ObjectMapperManager
                .DefaultInstance
                .GetMapper<CommonUniversalPropertyDTO, CommonUniversalProperty>()
                .Map(item);

            Database.CommonUniversalPropertyRepository.Create(result);

            return item;
        }

        /// <summary>
        /// Удалить объект по  идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public void Delete(long id)
        {
            Database.CommonUniversalPropertyRepository.Delete(id);
        }

        /// <summary>
        /// Удалить все объекты.
        /// </summary>
        public void DeleteAll()
        {
            Database.CommonUniversalPropertyRepository.DeleteAll();
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
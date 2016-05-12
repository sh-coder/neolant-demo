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
    public sealed class FacilityClassService : IDisposableRepository<FacilityClassDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="unitOfWork">Сервис для работы с сущностью.</param>
        public FacilityClassService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FacilityClassDTO> Get()
        {
            IEnumerable<FacilityClass> result = Database.FacilityClassRepository.Get();
            if (result == null)
            {
                return null;
            }

            ObjectsMapper<FacilityClass, FacilityClassDTO> mapper = ObjectMapperManager
                .DefaultInstance
                .GetMapper<FacilityClass, FacilityClassDTO>();

            var mappedResult = new Collection<FacilityClassDTO>();
            foreach (FacilityClass facilityClass in result)
            {
                mappedResult.Add(mapper.Map(facilityClass));
            }

            return mappedResult;
        }

        /// <summary>
        /// Получить объекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns></returns>
        public FacilityClassDTO Get(long id)
        {
            FacilityClass result = Database.FacilityClassRepository.Get(id);
            if (result == null)
            {
                return null;
            }

            FacilityClassDTO mappedResult = ObjectMapperManager
                .DefaultInstance
                .GetMapper<FacilityClass, FacilityClassDTO>()
                .Map(result);

            return mappedResult;
        }

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение фильтрации данных.</param>
        /// <returns></returns>
        public IEnumerable<FacilityClassDTO> Find(Expression<Func<FacilityClassDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать новый объект.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого объекта.</param>
        /// <returns></returns>
        public FacilityClassDTO Create(FacilityClassDTO item)
        {
            FacilityClass result = ObjectMapperManager
                .DefaultInstance
                .GetMapper<FacilityClassDTO, FacilityClass>()
                .Map(item);

            Database.FacilityClassRepository.Create(result);

            return item;
        }

        /// <summary>
        /// Удалить объект по  идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public void Delete(long id)
        {
            Database.FacilityClassRepository.Delete(id);
        }

        /// <summary>
        /// Удалить все объекты.
        /// </summary>
        public void DeleteAll()
        {
            Database.FacilityClassRepository.DeleteAll();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.Interfaces;

namespace NeolantDemo.BLL.Services
{
    /// <summary>
    /// Сервис CRUD операции для видов объектов.
    /// </summary>
    public sealed class FacilityService : IDisposableRepositoryWithHierarchy<FacilityDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="unitOfWork">Сервис для работы с сущностью.</param>
        public FacilityService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FacilityDTO> Get()
        {
            IEnumerable<Facility> result = Database.FacilityRepository.Get();
            IEnumerable<FacilityDTO> dto = ObjectMapperManager
                .DefaultInstance
                .GetMapper<IEnumerable<Facility>, IEnumerable<FacilityDTO>>()
                .Map(result);

            return dto;
        }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <returns></returns>
        public FacilityDTO Get(long id)
        {
            Facility result = Database.FacilityRepository.Get(id);
            if (result == null)
            {
                return null;
            }
            FacilityDTO dto = ObjectMapperManager.DefaultInstance.GetMapper<Facility, FacilityDTO>().Map(result);

            return dto;
        }

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение фильтрации данных.</param>
        /// <returns></returns>
        public IEnumerable<FacilityDTO> Find(Expression<Func<FacilityDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать новый объект.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого объекта.</param>
        /// <returns></returns>
        public FacilityDTO Create(FacilityDTO item)
        {
            Facility result = ObjectMapperManager.DefaultInstance.GetMapper<FacilityDTO, Facility>().Map(item);
            result = Database.FacilityRepository.Create(result);
            FacilityDTO dto = ObjectMapperManager.DefaultInstance.GetMapper<Facility, FacilityDTO>().Map(result);

            return dto;
        }

        /// <summary>
        /// Удалить объект по  идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        public void Delete(long id)
        {
            Database.FacilityRepository.Delete(id);
        }

        /// <summary>
        /// Удалить все объекты.
        /// </summary>
        public void DeleteAll()
        {
            Database.FacilityRepository.DeleteAll();
        }

        /// <summary>
        /// Получить иерархию потомков сущности.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns><see cref="FacilityDTO" /> или <c>null</c>.</returns>
        /// <value>Идентификатор сущности.</value>
        public FacilityDTO GetHierarchy(long id)
        {
            Facility result = Database.FacilityRepository.GetHierarchy(id);
            if (result == null)
            {
                return null;
            }

            FacilityDTO dto = ObjectMapperManager
                .DefaultInstance
                .GetMapper<Facility, FacilityDTO>(
                    new DefaultMapConfig().ConvertUsing(ConverterHierarhicalFacilityToFacilityDTO()))
                .Map(result);

            return dto;
        }

        /// <summary>
        /// Получить иерархию потомков сущности в виде списка.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        /// <returns></returns>
        public IEnumerable<FacilityDTO> GetHierarchyFlatten(long id)
        {
            IEnumerable<Facility> result = Database.FacilityRepository.GetHierarchyFlatten(id);
            if (result == null)
            {
                return null;
            }

            List<FacilityDTO> dto = ObjectMapperManager
                .DefaultInstance
                .GetMapper<List<Facility>, List<FacilityDTO>>()
                .Map(result.ToList());

            return dto;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Database.Dispose();
        }


        private static Func<Facility, FacilityDTO> ConverterHierarhicalFacilityToFacilityDTO()
        {
            return source =>
            {
                var facilityDTO = new FacilityDTO
                {
                    InstanceS = source.InstanceS,
                    ParentInstanceS = source.ParentInstanceS,
                    KindS = source.KindS,
                    Identifier = source.Identifier,
                };

                if (source.Children == null || source.Children.Count == 0)
                {
                    return facilityDTO;
                }

                facilityDTO.Children = new List<FacilityDTO>();
                foreach (BaseHierarchicalEntity baseHierarchicalEntity in source.Children)
                {
                    FacilityDTO mappedChild = ObjectMapperManager
                        .DefaultInstance
                        .GetMapper<Facility, FacilityDTO>(
                            new DefaultMapConfig().ConvertUsing(ConverterHierarhicalFacilityToFacilityDTO()))
                        .Map((Facility) baseHierarchicalEntity);

                    facilityDTO.Children.Add(mappedChild);
                }

                return facilityDTO;
            };
        }
    }
}
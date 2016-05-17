using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using EmitMapper;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.Interfaces;

namespace NeolantDemo.BLL.Services
{
    /// <summary>
    /// Сервис CRUD операции для объектов.
    /// </summary>
    public sealed class FacilityWithPropertiesService : IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="unitOfWork">Сервис для работы с сущностью.</param>
        public FacilityWithPropertiesService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <returns>IEnumerable&lt;<see cref="FacilityWithPropertiesDTO" />&gt; or <c>null</c>"/>.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<FacilityWithPropertiesDTO> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><see cref="FacilityWithPropertiesDTO" /> or <c>null</c>"/>.</returns>
        public FacilityWithPropertiesDTO Get(long id)
        {
            Facility facility = Database.FacilityRepository.Get(id);
            if (facility == null)
            {
                return null;
            }

            FacilityWithPropertiesDTO dto =
                ObjectMapperManager.DefaultInstance.GetMapper<Facility, FacilityWithPropertiesDTO>().Map(facility);

            IEnumerable<Property> properties = PropertyFactory.Create(Database, new BaseEntity[] {facility}).Properties;
            if (properties == null)
            {
                return dto;
            }

            ObjectsMapper<Property, PropertyDTO> mapper =
                ObjectMapperManager.DefaultInstance.GetMapper<Property, PropertyDTO>();
            dto.Properties = new List<PropertyDTO>();
            foreach (Property property in properties)
            {
                PropertyDTO dtoProperty = mapper.Map(property);
                dtoProperty.DynamicValue = PropertyFactory.GetPropertyDynamicValue(property);

                dto.Properties.Add(dtoProperty);
            }

            return dto;
        }

        /// <summary>
        /// Получает данные из репозитория по данному выражению.
        /// </summary>
        /// <param name="expression">Выражение фильтрации данных.</param>
        /// <returns></returns>
        public IEnumerable<FacilityWithPropertiesDTO> Find(Expression<Func<FacilityWithPropertiesDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать новый объект.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого объекта.</param>
        /// <returns><see cref="FacilityWithPropertiesDTO" /> or <c>null</c>"/>.</returns>
        public FacilityWithPropertiesDTO Create(FacilityWithPropertiesDTO item)
        {
            if (item == null)
            {
                return null;
            }

            Facility facility =
                ObjectMapperManager.DefaultInstance.GetMapper<FacilityWithPropertiesDTO, Facility>().Map(item);

            Facility isExists = Database.FacilityRepository.Get(facility.InstanceS);
            if (isExists != null)
            {
                return null;
            }

            //todo: Добавлять объект и свойства в одной транзакции
            facility = Database.FacilityRepository.Create(facility);
            FacilityWithPropertiesDTO result = ObjectMapperManager
                .DefaultInstance
                .GetMapper<Facility, FacilityWithPropertiesDTO>()
                .Map(facility);

            if (item.Properties == null || !item.Properties.Any())
            {
                return result;
            }

            // Получаем свойства доступные для данного вида оборудования.
            List<CommonUniversalProperty> aviableCommonUniversalProperties = Database
                .CommonUniversalPropertyRepository
                .Find(x => x.UniversalClassS == facility.KindS)
                .ToList();

            foreach (PropertyDTO propertyDTO in item.Properties)
            {
                CommonUniversalProperty cup = aviableCommonUniversalProperties
                    .FirstOrDefault(x => x.PropertyKindS == propertyDTO.PropertyKindS);
                if (cup == null) continue;

                PropertyKind propertyFromDatabase = Database.PropertyKindRepository.Get(cup.PropertyKindS);
                if (propertyFromDatabase == null) continue;

                // Установка типа свойства и привязка к объекту.
                propertyDTO.EntityType = propertyFromDatabase.NdtDataType;
                propertyDTO.TargetClassS = facility.InstanceS;
                SetPropertyStrongTypedValue(propertyDTO);

                Property property = ObjectMapperManager
                    .DefaultInstance
                    .GetMapper<PropertyDTO, Property>()
                    .Map(propertyDTO);
                // todo: Оптимизировать
                Database.PropertyRepository.Create(property);
            }

            return item;
        }

        /// <summary>
        /// Удалить объект по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объекта.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить иерархию потомков сущности.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        /// <returns></returns>
        public FacilityWithPropertiesDTO GetHierarchy(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить иерархию потомков сущности в виде списка.
        /// </summary>
        /// <value>Идентификатор сущности.</value>
        /// <returns></returns>
        public IEnumerable<FacilityWithPropertiesDTO> GetHierarchyFlatten(long id)
        {
            IEnumerable<Facility> facilities = Database.FacilityRepository.GetHierarchyFlatten(id);
            if (facilities == null)
            {
                return null;
            }

            List<Facility> facilitiesList = facilities.ToList();
            List<FacilityWithPropertiesDTO> mappedFacilities = ObjectMapperManager
                .DefaultInstance
                .GetMapper<List<Facility>, List<FacilityWithPropertiesDTO>>()
                .Map(facilitiesList);

            List<BaseEntity> baseEntities = facilitiesList.Cast<BaseEntity>().ToList();
            IEnumerable<Property> properties = PropertyFactory.Create(Database, baseEntities).Properties;
            if (properties == null)
            {
                return mappedFacilities;
            }

            ObjectsMapper<Property, PropertyDTO> propertyMapper = ObjectMapperManager
                .DefaultInstance
                .GetMapper<Property, PropertyDTO>();
            IList<Property> propertiesList = properties as IList<Property> ?? properties.ToList();
            foreach (FacilityWithPropertiesDTO facility in mappedFacilities)
            {
                IEnumerable<Property> facilityProperties = propertiesList
                    .Where(i => i.TargetClassS == facility.InstanceS)
                    .ToList();

                if (!facilityProperties.Any()) continue;

                facility.Properties = new List<PropertyDTO>();
                foreach (Property property in facilityProperties)
                {
                    PropertyDTO dtoProperty = propertyMapper.Map(property);
                    dtoProperty.DynamicValue = PropertyFactory.GetPropertyDynamicValue(property);

                    facility.Properties.Add(dtoProperty);
                }
            }

            return mappedFacilities;
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

        private static void SetPropertyStrongTypedValue(PropertyDTO propertyDTO)
        {
            if (!Enum.IsDefined(typeof (NamedDefinedTypeDataType), propertyDTO.EntityType))
            {
                throw new InvalidEnumArgumentException();
            }

            switch (propertyDTO.EntityType)
            {
                case NamedDefinedTypeDataType.String:
                    propertyDTO.StringValue = propertyDTO.DynamicValue;
                    return;
                case NamedDefinedTypeDataType.DateTime:
                    if (propertyDTO.DynamicValue is DateTime)
                    {
                        propertyDTO.DateTimeValue = propertyDTO.DynamicValue;
                    }
                    else
                    {
                        propertyDTO.DateTimeValue = DateTime.Parse(propertyDTO.DynamicValue);
                    }
                    return;
                case NamedDefinedTypeDataType.Double:
                    propertyDTO.FloatValue = propertyDTO.DynamicValue;
                    return;
                case NamedDefinedTypeDataType.Bigint:
                    propertyDTO.BigintValue = propertyDTO.DynamicValue;
                    return;
                case NamedDefinedTypeDataType.Int:
                    propertyDTO.IntegerValue = propertyDTO.DynamicValue;
                    return;
                case NamedDefinedTypeDataType.Decimal:
                    propertyDTO.DecimalValue = propertyDTO.DynamicValue;
                    return;
            }

            throw new NotImplementedException(string.Format(
                "NOT IMPLEMENTED SET NeolantDemo.BLL.DTO.PropertyDTO.DynamicValue for EntityType {0}",
                propertyDTO.EntityType));
        }
    }
}
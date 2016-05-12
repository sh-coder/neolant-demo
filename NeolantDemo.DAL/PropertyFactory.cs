using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.Interfaces;

namespace NeolantDemo.DAL
{
    /// <summary>
    /// Фабрика для работы со свойствами.
    /// </summary>
    public class PropertyFactory
    {
        [NotNull] private readonly IUnitOfWork _database;

        private PropertyFactory([NotNull] IUnitOfWork database)
        {
            _database = database;
        }

        /// <summary>
        /// Получает коллекцию свойств.
        /// <remarks>
        /// Инициализация происходит при создании.
        /// </remarks>
        /// </summary>
        /// <value>Перечисление свойств.</value>
        [CanBeNull]
        public IEnumerable<Property> Properties { get; private set; }


        /// <summary>
        /// Создать экземпляр фабрики для работы со свойствами.
        /// </summary>
        /// <param name="database">Интерфейс для доступа к бд.</param>
        /// <param name="baseEntities">Список сущностей.</param>
        /// <param name="propertyKinds">Список видов свойств.</param>
        /// <returns></returns>
        [NotNull]
        public static PropertyFactory Create(
            [NotNull] IUnitOfWork database,
            [CanBeNull] ICollection<BaseEntity> baseEntities,
            [CanBeNull] ICollection<PropertyKind> propertyKinds = null)
        {
            var pf = new PropertyFactory(database);
            pf.InitProperties(baseEntities, propertyKinds);

            return pf;
        }

        /// <summary>
        /// Получить значение свойства в виде <see cref="T:dynamic" />.
        /// </summary>
        /// <param name="property">Свойство.</param>
        /// <returns></returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public static dynamic GetPropertyDynamicValue(Property property)
        {
            if (!Enum.IsDefined(typeof (NamedDefinedTypeDataType), property.EntityType))
            {
                throw new InvalidEnumArgumentException();
            }

            switch (property.EntityType)
            {
                case NamedDefinedTypeDataType.String:
                    return property.StringValue;
                case NamedDefinedTypeDataType.DateTime:
                    return property.DateTimeValue;
                case NamedDefinedTypeDataType.Double:
                    return property.FloatValue;
                case NamedDefinedTypeDataType.Bigint:
                    return property.BigintValue;
                case NamedDefinedTypeDataType.Int:
                    return property.IntegerValue;
                case NamedDefinedTypeDataType.Decimal:
                    return property.DecimalValue;
            }

            throw new NotSupportedException(string.Format(
                "PropertyFactory.GetPropertyDynamicValue:: not supported for EntityType {0}",
                property.EntityType));
        }

        private void InitProperties(
            [CanBeNull] ICollection<BaseEntity> baseEntities,
            [CanBeNull] ICollection<PropertyKind> propertyKinds = null)
        {
            if (baseEntities == null || baseEntities.Count == 0)
            {
                return;
            }

            IEnumerable<long> baseEntitiesInstances = baseEntities.Select(i => i.InstanceS);
            if (propertyKinds == null || propertyKinds.Count == 0)
            {
                Properties = _database.PropertyRepository.Find(x => baseEntitiesInstances.Contains(x.TargetClassS));
                return;
            }

            IEnumerable<long> propertyKindInstances = propertyKinds.Select(i => i.InstanceS);

            Properties = _database.PropertyRepository.Find(x =>
                baseEntitiesInstances.Contains(x.TargetClassS)
                && propertyKindInstances.Contains(x.PropertyKindS));
        }
    }
}
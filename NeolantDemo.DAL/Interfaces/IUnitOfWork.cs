using System;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL.Entities;

namespace NeolantDemo.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Получает репозиторий данных типа <see cref="Facility" />.
        /// </summary>
        IRepositoryWithHierarchy<Facility> FacilityRepository { get; }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="FacilityClass" />.
        /// </summary>
        IRepository<FacilityClass> FacilityClassRepository { get; }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="PropertyKind" />.
        /// </summary>
        IRepository<PropertyKind> PropertyKindRepository { get; }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="CommonUniversalProperty" />.
        /// </summary>
        IRepository<CommonUniversalProperty> CommonUniversalPropertyRepository { get; }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="Property" />.
        /// </summary>
        IRepository<Property> PropertyRepository { get; }
    }
}
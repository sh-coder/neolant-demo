using System;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.DAL.Entities;
using NeolantDemo.DAL.Interfaces;
using NeolantDemo.DAL.MSSQL;

namespace NeolantDemo.DAL.Repositories
{
    /// <summary>
    /// Реализация UnitOfWork.
    /// </summary>
    /// <seealso cref="NeolantDemo.DAL.Interfaces.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MSSQLCustomContext _databaseContext;
        private IRepository<CommonUniversalProperty> _commonUniversalPropertyRepository;
        private bool _disposed;
        private IRepository<FacilityClass> _facilityClassRepository;
        private IRepositoryWithHierarchy<Facility> _generalFacilityRepository;
        private IRepository<PropertyKind> _propertyKindRepository;
        private IRepository<Property> _propertyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public UnitOfWork(string connectionString)
        {
            _databaseContext = new MSSQLCustomContext(connectionString);
        }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="Facility" />.
        /// </summary>
        public IRepositoryWithHierarchy<Facility> FacilityRepository
        {
            get
            {
                return _generalFacilityRepository ??
                       (_generalFacilityRepository = new EntityRepositoryWithHierarchy<Facility>(_databaseContext));
            }
        }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="FacilityClass" />.
        /// </summary>
        public IRepository<FacilityClass> FacilityClassRepository
        {
            get
            {
                _facilityClassRepository = null;
                return _facilityClassRepository ??
                       (_facilityClassRepository = new EntityRepositoryWithHierarchy<FacilityClass>(_databaseContext));
            }
        }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="PropertyKind" />.
        /// </summary>
        public IRepository<PropertyKind> PropertyKindRepository
        {
            get
            {
                return _propertyKindRepository ??
                       (_propertyKindRepository = new EntityRepositoryWithHierarchy<PropertyKind>(_databaseContext));
            }
        }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="CommonUniversalProperty" />.
        /// </summary>
        public IRepository<CommonUniversalProperty> CommonUniversalPropertyRepository
        {
            get
            {
                return _commonUniversalPropertyRepository ??
                       (_commonUniversalPropertyRepository =
                           new EntityRepository<CommonUniversalProperty>(_databaseContext));
            }
        }

        /// <summary>
        /// Получает репозиторий данных типа <see cref="Property" />.
        /// </summary>
        public IRepository<Property> PropertyRepository
        {
            get
            {
                return _propertyRepository ??
                       (_propertyRepository =
                           new EntityRepository<Property>(_databaseContext));
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _databaseContext.Dispose();
            }
            _disposed = true;
        }
    }
}
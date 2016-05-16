using NeolantDemo.BLL.DTO;
using NeolantDemo.BLL.Services;
using NeolantDemo.Core.Interfaces;
using Ninject.Modules;

namespace NeolantDemo.WEB
{
    /// <summary>
    /// Настройки зависимостей (IoC).
    /// </summary>
    public class NinjectBindingsModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IDisposableRepositoryWithHierarchy<FacilityDTO>>()
                .To<FacilityService>();
            Bind<IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO>>()
                .To<FacilityWithPropertiesService>();
            Bind<IDisposableRepository<PropertyKindDTO>>()
                .To<PropertyKindService>();
            Bind<IDisposableRepository<FacilityClassDTO>>()
                .To<FacilityClassService>();
            Bind<IDisposableRepository<CommonUniversalPropertyDTO>>()
                .To<CommonUniversalPropertyService>();
        }
    }
}
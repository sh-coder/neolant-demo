using System.Web.OData.Routing;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;

namespace NeolantDemo.WEB.Controllers
{
    /// <summary>
    /// Контролер CRUD операций для вида объекта.
    /// </summary>
    [ODataRoutePrefix(RoutesConfig.ControllerFacilityClasses)]
    public class FacilityClassesController : BaseODataCRUDController<FacilityClassDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="repository">Сервис для работы с сущностью.</param>
        public FacilityClassesController(IDisposableRepository<FacilityClassDTO> repository)
            : base(repository)
        {
        }
    }
}
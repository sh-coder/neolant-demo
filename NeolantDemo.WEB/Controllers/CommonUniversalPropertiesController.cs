using System.Web.OData.Routing;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;

namespace NeolantDemo.WEB.Controllers
{
    /// <summary>
    /// Контролер CRUD операций для сущности связи вида объекта с видом свойств.
    /// </summary>
    [ODataRoutePrefix(RoutesConfig.ControllerCommonUniversalProperties)]
    public class CommonUniversalPropertiesController : BaseODataCRUDController<CommonUniversalPropertyDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="repository">Сервис для работы с сущностью.</param>
        public CommonUniversalPropertiesController(IDisposableRepository<CommonUniversalPropertyDTO> repository)
            : base(repository)
        {
        }
    }
}
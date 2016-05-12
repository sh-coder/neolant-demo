using System.Web.OData.Routing;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;

namespace NeolantDemo.WEB.Controllers
{
    /// <summary>
    /// Контролер CRUD операций для вида свойств/атрибутов.
    /// </summary>
    [ODataRoutePrefix(RoutesConfig.ControllerPropertyKinds)]
    public class PropertyKindsController : BaseODataCRUDController<PropertyKindDTO>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="repository">Сервис для работы с сущностью.</param>
        public PropertyKindsController(IDisposableRepository<PropertyKindDTO> repository)
            : base(repository)
        {
        }
    }
}
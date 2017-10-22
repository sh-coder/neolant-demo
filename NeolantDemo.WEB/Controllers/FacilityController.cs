using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using EmitMapper;
using Microsoft.OData;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.WEB.Models;
using Swashbuckle.Swagger.Annotations;

namespace NeolantDemo.WEB.Controllers
{
    /// <summary>
    /// Контроллер для работы с объектом.
    /// </summary>
    [ODataRoutePrefix(RoutesConfig.ControllerFacilities)]
    public class FacilityController : ODataController
    {
        private readonly IDisposableRepositoryWithHierarchy<FacilityDTO> _repository;
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings
        {
            MaxExpansionDepth = 10,
            AllowedQueryOptions = AllowedQueryOptions.Expand | AllowedQueryOptions.Select
        };

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="repository">Сервис для работы с сущностью.</param>
        public FacilityController(IDisposableRepositoryWithHierarchy<FacilityDTO> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение поддерева объектов по заданному объекту (только наименования и иерархия).
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <param name="queryOptions">Опции запроса.</param>
        /// <returns></returns>
        [HttpGet]
        [ODataRoute(RoutesConfig.RouteFacilityFunctionHierarchy)]
        [SwaggerResponse(HttpStatusCode.OK, null, typeof (FacilityHierarchy))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public IHttpActionResult GetHierarchy(long instanceS, ODataQueryOptions<FacilityHierarchy> queryOptions)
        {
            try
            {
                queryOptions.Validate(ValidationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            FacilityDTO result = _repository.GetHierarchy(instanceS);
            if (result == null)
            {
                return NotFound();
            }
            if (result.Children == null || !result.Children.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            FacilityHierarchy facilityHierarchy = ObjectMapperManager
                .DefaultInstance
                .GetMapper<FacilityDTO, FacilityHierarchy>()
                .Map(result);

            return Ok(facilityHierarchy);
        }
    }
}
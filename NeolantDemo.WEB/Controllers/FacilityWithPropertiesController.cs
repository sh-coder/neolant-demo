using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using EmitMapper;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.WEB.Models;
using Swashbuckle.Swagger.Annotations;

namespace NeolantDemo.WEB.Controllers
{
    /// <summary>
    /// Контролер для чтения и создания объекта со свойствами.
    /// </summary>
    [ODataRoutePrefix(RoutesConfig.ControllerFacilityWithProperties)]
    public class FacilityWithPropertiesController : ODataController
    {
        private readonly IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO> _repository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="repository">Сервис для работы с сущностью.</param>
        public FacilityWithPropertiesController(IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить объект со свойствами.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <returns></returns>
        [HttpGet]
        [ODataRoute(RoutesConfig.ODataParameterInstanceS)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof (FacilityWithProperties))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [EnableQuery]
        public IHttpActionResult Get(long instanceS)
        {
            FacilityWithPropertiesDTO facilityWithPropertiesDTO = _repository.Get(instanceS);
            if (facilityWithPropertiesDTO == null)
            {
                return NotFound();
            }

            FacilityWithProperties facility = ObjectMapperManager
                .DefaultInstance.GetMapper<FacilityWithPropertiesDTO, FacilityWithProperties>()
                .Map(facilityWithPropertiesDTO);

            return Ok(facility);
        }

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого сущности.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        [ODataRoute]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof (FacilityWithProperties))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Post(FacilityWithProperties item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FacilityWithPropertiesDTO facilityWithPropertiesDTO = ObjectMapperManager
                .DefaultInstance
                .GetMapper<FacilityWithProperties, FacilityWithPropertiesDTO>()
                .Map(item);

            facilityWithPropertiesDTO = _repository.Create(facilityWithPropertiesDTO);

            item = ObjectMapperManager
                .DefaultInstance.GetMapper<FacilityWithPropertiesDTO, FacilityWithProperties>()
                .Map(facilityWithPropertiesDTO);

            return Created(item);
        }

        /// <summary>
        /// Для объекта с идентификатором <paramref name="instanceS" /> получить потомков вида заданного идентификатором
        /// <paramref name="kindS" />.
        /// </summary>
        /// <param name="instanceS">Идентификатор объекта.</param>
        /// <param name="kindS">Идентификатор вида объекта.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpGet]
        [ODataRoute(RoutesConfig.RouteFacilityWithPropertiesDescendantsFilterByKind)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof (IQueryable<FacilityWithProperties>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [EnableQuery]
        public IHttpActionResult GetDescendants(long instanceS, long? kindS)
        {
            IEnumerable<FacilityWithPropertiesDTO> result = _repository.GetHierarchyFlatten(instanceS);
            if (result == null)
            {
                return NotFound();
            }

            List<FacilityWithPropertiesDTO> resultList = kindS.HasValue
                ? result.AsParallel().Where(x => x.KindS == kindS.Value).ToList()
                : result.ToList();

            if (!resultList.Any())
            {
                return NotFound();
            }

            IQueryable<FacilityWithProperties> mappedResult = ObjectMapperManager
                .DefaultInstance
                .GetMapper<List<FacilityWithPropertiesDTO>, List<FacilityWithProperties>>()
                .Map(resultList)
                .AsQueryable();

            return Ok(mappedResult);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using NeolantDemo.Core.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace NeolantDemo.WEB.Controllers
{
    /// <summary>
    /// Базовый контроллер для CRUD операций.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Web.OData.ODataController" />
    public abstract class BaseODataCRUDController<T> : ODataController where T : class
    {
        private readonly IDisposableRepository<T> _repository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="repository">Сервис для работы с сущностью.</param>
        protected BaseODataCRUDController(IDisposableRepository<T> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ODataRoute]
        [EnableQuery(AllowedQueryOptions =
            AllowedQueryOptions.Count |
            AllowedQueryOptions.Filter |
            AllowedQueryOptions.OrderBy |
            AllowedQueryOptions.Select |
            AllowedQueryOptions.Skip |
            AllowedQueryOptions.Top)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public IHttpActionResult Get()
        {
            IEnumerable<T> result = _repository.Get();
            if (result == null)
            {
                return NotFound();
            }
            IList<T> resultToList = result as IList<T> ?? result.ToList();
            if (resultToList.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(resultToList);
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="instanceS">Идентификатор сущности.</param>
        /// <returns></returns>
        [HttpGet]
        [ODataRoute(RoutesConfig.ODataParameterInstanceS)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [EnableQuery]
        public IHttpActionResult Get(long instanceS)
        {
            T result = _repository.Get(instanceS);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="item">Экземпляр создаваемого сущности.</param>
        /// <returns></returns>
        [HttpPost]
        [ODataRoute]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Post(T item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            T result = _repository.Create(item);

            return Created(result);
        }

        /// <summary>
        /// Удалить сущность по идентификатору.
        /// </summary>
        /// <param name="instanceS">Идентификатор сущности.</param>
        /// <returns></returns>
        [HttpDelete]
        [ODataRoute(RoutesConfig.ODataParameterInstanceS)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent, null, typeof (void))]
        public IHttpActionResult Delete(long instanceS)
        {
            _repository.Delete(instanceS);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Results;
using Microsoft.OData.Edm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeolantDemo.BLL.DTO;
using NeolantDemo.Core.Interfaces;
using NeolantDemo.WEB.Controllers;
using NeolantDemo.WEB.Models;
using Ninject;

namespace NeolantDemo.WEB.Tests
{
    [TestClass]
    public class UnitTestFacility
    {
        private const string BaseUrl = "http://localhost:20449/";

        private const long FcPumpInstanceS = 7045466727029669888;
        private const long FcTubeInstanceS = 7827039262570627072;
        private const long PkInstallationDateInstanceS = 2048281056983318528;
        private const long PkWeightInstanceS = 4539770964740669440;
        private const long PkDiameterInstanceS = 372698464208408832;
        private const long PkSteelGradeInstanceS = 7649422914993258496;

        private readonly IEdmModel _edmModel;
        private readonly IDisposableRepositoryWithHierarchy<FacilityDTO> _facilityRepository;
        private readonly IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO> _facilityWithPropertiesRepository;

        public UnitTestFacility()
        {
            var ninjectKernel = new StandardKernel(NinjectConfiguration.Modules);
            _facilityWithPropertiesRepository =
                ninjectKernel.Get<IDisposableRepositoryWithHierarchy<FacilityWithPropertiesDTO>>();
            _facilityRepository = ninjectKernel.Get<IDisposableRepositoryWithHierarchy<FacilityDTO>>();

            var httpConfig = new HttpConfiguration();
            _edmModel = EdmModelFactory.CreateModel(httpConfig);
        }

        [TestMethod]
        public void TestFacilityWithPropertiesController()
        {
            var item1 = new FacilityWithProperties
            {
                InstanceS = 1,
                ParentInstanceS = null,
                KindS = FcPumpInstanceS,
                Identifier = "Тестовый насос 1",
                Properties = new List<Property>
                {
                    new Property {PropertyKindS = PkInstallationDateInstanceS, DynamicValue = DateTime.Now.AddDays(-1)},
                    new Property {PropertyKindS = PkWeightInstanceS, DynamicValue = 25.0D}
                }
            };

            var item2 = new FacilityWithProperties
            {
                InstanceS = 2,
                ParentInstanceS = item1.InstanceS,
                KindS = FcPumpInstanceS,
                Identifier = "Тестовый насос 2",
                Properties = new List<Property>
                {
                    new Property {PropertyKindS = PkInstallationDateInstanceS, DynamicValue = DateTime.Now.AddDays(-1)},
                    new Property {PropertyKindS = PkWeightInstanceS, DynamicValue = 30.0D}
                }
            };

            var item3 = new FacilityWithProperties
            {
                InstanceS = 3,
                ParentInstanceS = item2.InstanceS,
                KindS = FcPumpInstanceS,
                Identifier = "Тестовый насос 3",
                Properties = new List<Property>
                {
                    new Property {PropertyKindS = PkInstallationDateInstanceS, DynamicValue = DateTime.Now.AddDays(-1)},
                    new Property {PropertyKindS = PkWeightInstanceS, DynamicValue = 10.0D}
                }
            };

            var item4 = new FacilityWithProperties
            {
                InstanceS = 4,
                ParentInstanceS = item2.InstanceS,
                KindS = FcTubeInstanceS,
                Identifier = "Тестовая труба",
                Properties = new List<Property>
                {
                    new Property {PropertyKindS = PkDiameterInstanceS, DynamicValue = 340},
                    new Property {PropertyKindS = PkSteelGradeInstanceS, DynamicValue = "10A"}
                }
            };


            // POST
            TestPost(item1);
            TestPost(item2);
            TestPost(item3);
            TestPost(item4);


            // GET
            TestGet(item1);
            TestGet(item2);
            TestGet(item3);
            TestGet(item4);


            // GET Descendants
            // Потомки без указания вида.
            IHttpActionResult response = GetDescendantsResponse(item1.InstanceS, null);
            var responseResult = response as OkNegotiatedContentResult<IQueryable<FacilityWithProperties>>;
            Assert.IsNotNull(responseResult);

            List<FacilityWithProperties> responseResultList = responseResult.Content.ToList();
            Assert.IsInstanceOfType(responseResultList, typeof (List<FacilityWithProperties>));
            Assert.IsTrue(responseResultList.Count == 4);
            //todo: Почему то не работает responseResultList.Contains(item1)
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item1.InstanceS));
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item2.InstanceS));
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item3.InstanceS));
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item4.InstanceS));

            // Потомки с указанием вида.
            response = GetDescendantsResponse(item1.InstanceS, FcPumpInstanceS);
            responseResult = response as OkNegotiatedContentResult<IQueryable<FacilityWithProperties>>;
            Assert.IsNotNull(responseResult);

            responseResultList = responseResult.Content.ToList();
            Assert.IsInstanceOfType(responseResultList, typeof (List<FacilityWithProperties>));
            Assert.IsTrue(responseResultList.Count == 3);
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item1.InstanceS));
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item2.InstanceS));
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item3.InstanceS));

            response = GetDescendantsResponse(item1.InstanceS, 0);
            Assert.IsInstanceOfType(response, typeof (NotFoundResult));

            // Потомки без указания вида + фильтр
            const string filterFormat =
                "?$filter=properties/any(x: x/propertyKindS eq {0} and x/dateTimeValue lt {1}) and properties/any(y: y/propertyKindS eq {2} and y/floatValue gt {3})";
            string filter = string.Format(filterFormat,
                PkInstallationDateInstanceS,
                DateTime.Now.ToString("u").Replace(" ", "T"),
                PkWeightInstanceS,
                10.0D);

            response = GetDescendantsResponse(item1.InstanceS, null, filter);
            responseResult = response as OkNegotiatedContentResult<IQueryable<FacilityWithProperties>>;
            Assert.IsNotNull(responseResult);
            responseResultList = responseResult.Content.ToList();
            Assert.IsInstanceOfType(responseResultList, typeof (List<FacilityWithProperties>));
            Assert.IsTrue(responseResultList.Count == 2);
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item1.InstanceS));
            Assert.IsTrue(responseResultList.Any(i => i.InstanceS == item2.InstanceS));


            // GET Получение иерархии объектов
            response = GetHierarchyResponse(item1.InstanceS);
            var responseResultFacilityHierarchy = response as OkNegotiatedContentResult<FacilityHierarchy>;
            Assert.IsNotNull(responseResultFacilityHierarchy);

            FacilityHierarchy item = responseResultFacilityHierarchy.Content;
            Assert.IsInstanceOfType(item, typeof (FacilityHierarchy));

            Assert.IsTrue(item.InstanceS == item1.InstanceS);
            Assert.IsTrue(item.Children.Count == 1);
            Assert.IsTrue(item.Children.Exists(i => i.InstanceS == item2.InstanceS));
            Assert.IsTrue(item.Children[0].Children.Count == 2);
            Assert.IsTrue(item.Children[0].Children.Any(i => i.InstanceS == item3.InstanceS));
            Assert.IsTrue(item.Children[0].Children.Any(i => i.InstanceS == item4.InstanceS));
        }

        private void TestPost(FacilityWithProperties item)
        {
            var controller = new FacilityWithPropertiesController(_facilityWithPropertiesRepository);

            IHttpActionResult response = controller.Post(item);
            var responceResult = response as CreatedODataResult<FacilityWithProperties>;

            Assert.IsNotNull(responceResult);
            Assert.IsNotNull(responceResult.Entity);
            Assert.AreEqual(responceResult.Entity, item);
        }

        private void TestGet(FacilityWithProperties item)
        {
            var controller = new FacilityWithPropertiesController(_facilityWithPropertiesRepository);

            IHttpActionResult response = controller.Get(item.InstanceS);
            var responceResult = response as OkNegotiatedContentResult<FacilityWithProperties>;

            Assert.IsNotNull(responceResult);
            Assert.IsNotNull(responceResult.Content);

            // MSSQL сервер округляет миллисекунды, поэтому корректируем время.
            foreach (Property property in item.Properties)
            {
                if (property.DynamicValue is DateTime)
                {
                    var sqlDateTime = new SqlDateTime(property.DynamicValue);
                    property.DynamicValue = sqlDateTime.Value;
                }
            }
            Assert.AreEqual(responceResult.Content, item);
        }

        private IHttpActionResult GetDescendantsResponse(long instanceS, long? kindS, string filter = "")
        {
            string url = BaseUrl + RoutesConfig.GetRouteFacilityWithPropertiesDescendantsFilterByKind(instanceS, kindS) +
                         filter;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var context = new ODataQueryContext(_edmModel, typeof (FacilityWithProperties), null);
            var queryOptions = new ODataQueryOptions<FacilityWithProperties>(context, request);

            var controller = new FacilityWithPropertiesController(_facilityWithPropertiesRepository);
            IHttpActionResult response = controller.GetDescendants(instanceS, kindS, queryOptions);

            return response;
        }

        private IHttpActionResult GetHierarchyResponse(long instanceS)
        {
            const string filter = "?$expand=children($levels=max; $select=identifier)&$select=identifier";
            string url = BaseUrl + RoutesConfig.GetRouteFacilityFunctionHierarchy(instanceS) + filter;
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var context = new ODataQueryContext(_edmModel, typeof (FacilityHierarchy), null);
            var queryOptions = new ODataQueryOptions<FacilityHierarchy>(context, request);

            var controller = new FacilityController(_facilityRepository);
            IHttpActionResult response = controller.GetHierarchy(instanceS, queryOptions);

            return response;
        }
    }
}
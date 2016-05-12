using System.Web.Http;
using System.Web.OData.Builder;
using Microsoft.OData.Edm;
using NeolantDemo.BLL.DTO;

namespace NeolantDemo.WEB.Models
{
    /// <summary>
    /// Class EdmModelFactory.
    /// </summary>
    public static class EdmModelFactory
    {
        private static IEdmModel _model;

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="config">Global configuration.</param>
        /// <returns></returns>
        public static IEdmModel CreateModel(HttpConfiguration config)
        {
            return _model ?? (_model = GetModel(config));
        }

        private static IEdmModel GetModel(HttpConfiguration config)
        {
            var builder = new ODataConventionModelBuilder(config)
            {
                Namespace = RoutesConfig.Namespace
            };
            builder.EnableLowerCamelCase(NameResolverOptions.ProcessReflectedPropertyNames);

            builder.EntitySet<FacilityClassDTO>(RoutesConfig.ControllerFacilityClasses);
            builder.EntitySet<PropertyKindDTO>(RoutesConfig.ControllerPropertyKinds);
            builder.EntitySet<CommonUniversalPropertyDTO>(RoutesConfig.ControllerCommonUniversalProperties);
            builder.ComplexType<Property>();

            builder.EntitySet<FacilityHierarchy>(RoutesConfig.ControllerFacilities)
                .EntityType
                .Action(RoutesConfig.FunctionHierarchy)
                .ReturnsFromEntitySet<FacilityHierarchy>(RoutesConfig.ControllerFacilities);

            builder.EntitySet<FacilityWithProperties>(RoutesConfig.ControllerFacilityWithProperties)
                .EntityType
                .Function(RoutesConfig.FunctionDescendants)
                .ReturnsCollectionFromEntitySet<FacilityWithProperties>(RoutesConfig.ControllerFacilityWithProperties)
                .Parameter<long>(RoutesConfig.KindS)
                .OptionalParameter = true;

            IEdmModel edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
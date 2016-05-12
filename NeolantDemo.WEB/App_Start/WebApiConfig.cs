using System;
using System.Web.Http;
using System.Web.OData.Extensions;
using NeolantDemo.WEB.Models;

namespace NeolantDemo.WEB
{
    /// <summary>
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.SetTimeZoneInfo(TimeZoneInfo.Utc);
            config.EnableUnqualifiedNameCall(true);
            config.MapODataServiceRoute(
                "odata",
                RoutesConfig.BaseUrl,
                EdmModelFactory.CreateModel(config));
        }
    }
}
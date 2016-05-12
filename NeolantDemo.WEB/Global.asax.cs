using System.Web;
using System.Web.Http;

namespace NeolantDemo.WEB
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                FormatterConfig.Register(config);
                WebApiConfig.Register(config);
            });
        }
    }
}
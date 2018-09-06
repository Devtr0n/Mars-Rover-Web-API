using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Owin;
using MarsRoverExample.Repositories;
using MarsRoverExample.Services;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;
using System.Linq;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(MarsRoverExample.Startup))]

namespace MarsRoverExample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // inject our dependencies
            var config = new HttpConfiguration
            {
                DependencyResolver = new NinjectResolver(CreateKernel())
            };

            // map the attribute-defined routes for the application
            config.MapHttpAttributeRoutes();

            // enable 'Swagger' API documentation
            config.EnableSwagger(c => c.SingleApiVersion("v1", "Mars Rover API")).EnableSwaggerUi();

            // keep API 'default'
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // ensure 'JSON' responses only
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // let's get configured
            app.UseWebApi(config);
        }

        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IMarsRoverRepository>().ToConstant(new MarsRoverRepository());
            kernel.Bind<IMarsRoverMapper>().To<MarsRoverMapper>().InRequestScope();

            return kernel;
        }
    }
}

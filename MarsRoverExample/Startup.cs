using MarsRoverExample.Repositories;
using MarsRoverExample.Services;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Owin;
using Swashbuckle.Application;
using System.Linq;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;

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
            config
                .EnableSwagger(c =>
                {
                    //c.RootUrl(req => SwaggerDocsConfig.DefaultRootUrlResolver(req)); //this doesn't work because...OWIN? I need to upgrade to .Core...
                    c.SingleApiVersion("v1", "Mars Rover API").Description("This is Richard Hollon's Mars Rover Web API example using: C#, .NET Framework 4.6.1, JSON, OWIN, Ninject DI, RESTful Web API services and Swagger documentation.");
                }).EnableSwaggerUi();

            // keep API routing 'default'
            config.Routes.MapHttpRoute(
                name: "MarsRoverApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // ensure 'JSON' responses only
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // let's get configured now
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

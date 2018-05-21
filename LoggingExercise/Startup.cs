using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using LoggingExercise.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LoggingExercise.Startup))]

namespace LoggingExercise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.Configure(GlobalFilters.Filters);

        }
    }
}

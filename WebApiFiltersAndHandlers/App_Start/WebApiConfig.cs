using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebApiFiltersAndHandlers.ActionFilters;
using WebApiFiltersAndHandlers.DelegatingHandlers;
using WebApiFiltersAndHandlers.ExceptionHandler;

namespace WebApiFiltersAndHandlers
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Register the global exception handler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Register delegating handlers
            config.MessageHandlers.Add(new FirstDelegatingHandler());
            config.MessageHandlers.Add(new SecondDelegatingHandler());

            // Register action filters
            config.Filters.Add(new FirstActionFilter());
            config.Filters.Add(new SecondActionFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

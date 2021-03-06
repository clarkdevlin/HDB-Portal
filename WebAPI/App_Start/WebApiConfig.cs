﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //EnableCorsAttribute cors = new EnableCorsAttribute("https://isp.ecics.com.sg,http://localhost:4321","Accept,Content","GET,POST")
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*"); //this will allow any webiste of any header and of any method
            config.EnableCors();

            config.Filters.Add(new RequireHttpsAttribute());

            //Apply Basic Authentican globaly
            //config.Filters.Add(new BasicAuthenticationAttribute());
        }
    }
}

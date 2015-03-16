using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using JollyDragon.Domain;
using Microsoft.Owin;
using Owin;
using Pocket;

[assembly: OwinStartup(typeof(JollyDragon.Startup))]
namespace JollyDragon
{
    
    public class Startup
    {
        private PocketContainer _container;

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            _container = new PocketContainer();
            config.DependencyResolver = new PocketContainerDependencyResolver(_container);

            ConfigureContainer();

            app.UseWebApi(config);
            
        }

        private void ConfigureContainer()
        {
            _container.RegisterSingle(c => new ConcurrentDictionary<string, Session>());
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingModule.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using System.Reflection;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

using Rocket.Core.Diagnostics;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services;
using Rocket.Routing.Services.Contracts;

using Module = Autofac.Module;

namespace Rocket.Routing.IoC.Autofac
{
    [Export(typeof(IBootstrapper))]
    public class RoutingModule : Module, IBootstrapper
    {
        //private HttpConfiguration _httpConfiguration;

        //public HttpConfiguration HttpConfiguration
        //{
        //    get
        //    {
        //        return _httpConfiguration;
        //    }
        //    set
        //    {
        //        _httpConfiguration = value;
        //    }
        //}

        //public RoutingModule()
        //{
        //}

        //private RoutingModule(
        //    HttpConfiguration httpConfiguration)
        //{
        //    _httpConfiguration = httpConfiguration;
        //}

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly
                .GetAssembly(typeof(VersionedRouteAttribute));

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PreserveExistingDefaults();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Provider"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PreserveExistingDefaults();

            builder
                .RegisterType<AcceptHeaderParserService>()
                .As<IHeaderParserService<AcceptHeader>>()
                .InstancePerRequest()
                .PreserveExistingDefaults();

            builder
                .Register(b => new Log("Rocket.Routing"))
                .As<ILog>()
                .InstancePerRequest()
                .PreserveExistingDefaults();
        }

        public void Configure(HttpConfiguration configuration)
        {
            var containerBuilder = new ContainerBuilder();

            var x = (AutofacWebApiDependencyResolver)configuration.DependencyResolver;


            //containerBuilder
            //    .RegisterModule(new RoutingModule(configuration));
            
            containerBuilder
                .RegisterModule(this);

            IContainer c = (IContainer)x.Container;

            containerBuilder.Update(c);
        }
    }
}
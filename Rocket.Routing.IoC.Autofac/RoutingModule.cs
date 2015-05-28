// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingModule.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
        public void Configure(HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var dependencyResolver = configuration
                .DependencyResolver as AutofacWebApiDependencyResolver;

            if (dependencyResolver == null)
            {
                return;
            }

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(this);

            var container = dependencyResolver
                .Container as IContainer;

            if (container == null)
            {
                return;
            }

            containerBuilder.Update(container);
        }

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
    }
}
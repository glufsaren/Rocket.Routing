// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapBootstrapper.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the StructureMapBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Web.Http;

using Rocket.Core.Diagnostics;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services;
using Rocket.Routing.Services.Contracts;

using StructureMap;
using StructureMap.Configuration.DSL;

namespace Rocket.Routing.IoC.StructureMap
{
    [Export(typeof(IBootstrapper))]
    public class StructureMapBootstrapper : Registry, IBootstrapper
    {
        public void Configure(HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var dependencyResolver = configuration
                .DependencyResolver as StructureMapResolver;

            if (dependencyResolver == null)
            {
                return;
            }

            var container = dependencyResolver.Container;

            Configure(container);

            container.Configure(
                expression => expression.AddRegistry(this));
        }

        private void Configure(IContainer container)
        {
            if (!container.Model
                .HasImplementationsFor<IHeaderParserService<AcceptHeader>>())
            {
                For<IHeaderParserService<AcceptHeader>>()
                    .Use<AcceptHeaderParserService>()
                    .Transient();
            }

            if (!container.Model.HasImplementationsFor<IAcceptHeaderPatternService>())
            {
                For<IAcceptHeaderPatternService>()
                    .Use<AcceptHeaderPatternService>()
                    .Transient();
            }

            if (!container.Model.HasImplementationsFor<IVendorNameService>())
            {
                For<IVendorNameService>()
                    .Use<DefaultVendorNameService>()
                    .Transient();
            }

            if (!container.Model
                .HasImplementationsFor<IHttpRequestMessageResolverService>())
            {
                For<IHttpRequestMessageResolverService>()
                    .Use<HttpRequestMessageResolverService>()
                    .Transient();
            }

            if (!container.Model.HasImplementationsFor<IRequestIdService>())
            {
                For<IRequestIdService>()
                    .Use<RequestIdService>()
                    .Transient();
            }

            if (!container.Model
                .HasImplementationsFor<IAcceptHeaderStoreService>())
            {
                For<IAcceptHeaderStoreService>()
                    .Use<RequestPropertiesAcceptHeaderStoreService>()
                    .Transient();
            }

            if (!container.Model.HasImplementationsFor<IRoutingService>())
            {
                For<IRoutingService>()
                    .Use<RoutingService>()
                    .Transient();
            }

            if (!container.Model.HasImplementationsFor<ILog>())
            {
                For<ILog>()
                    .Use(context => new Log("Rocket.Routing"))
                    .AlwaysUnique()
                    .Transient();
            }
        }
    }
}
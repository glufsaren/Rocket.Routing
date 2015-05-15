// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutofacConfig.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AutofacConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

using Autofac;
using Autofac.Integration.WebApi;

using Rocket.Routing.IoC.Autofac;
using Rocket.Routing.Services.Contracts;
using Rocket.Test;

namespace Rocket.Routing.Test.Component
{
    public class AutofacConfig : IDependencyResolverConfig
    {
        private readonly HttpServerHost _httpServerHost;

        public AutofacConfig(HttpServerHost httpServerHost)
        {
            _httpServerHost = httpServerHost;
        }

        public HttpConfiguration HttpConfiguration { private get; set; }

        public IDependencyResolver Configure()
        {
            var builder = new ContainerBuilder();

            var executingAssembly = Assembly
                .GetAssembly(typeof(VersionedRouteAttribute));

            builder
                .RegisterApiControllers(executingAssembly);

            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(t => !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t));

            builder
                .Register(o => new SelfHostHttpRequestMessageResolver(_httpServerHost))
                .As<IHttpRequestMessageResolverService>();

            var container = builder.Build();

            return new AutofacWebApiDependencyResolver(container);
        }
    }
}
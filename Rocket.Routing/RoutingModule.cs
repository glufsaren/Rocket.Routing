// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingModule.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using System.Web.Http;

using Autofac;

using Rocket.Core.Diagnostics;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services;
using Rocket.Routing.Services.Contracts;

using Module = Autofac.Module;

namespace Rocket.Routing
{
    public class RoutingModule : Module
    {
        private readonly HttpConfiguration _httpConfiguration;

        public RoutingModule(
            HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            _httpConfiguration.MessageHandlers
                .Add(new MessageHeadersHandler());

            var assembly = Assembly.GetExecutingAssembly();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Provider"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterType<AcceptHeaderParserService>()
                .As<IHeaderParserService<AcceptHeader>>()
                .InstancePerRequest();

            builder
                .Register(b => new Log("Rocket.Routing"))
                .As<ILog>()
                .InstancePerRequest();
        }
    }
}
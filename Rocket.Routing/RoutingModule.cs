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

using Rocket.Core.Configuration;
using Rocket.Core.Diagnostics;

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
                .Where(t => t.Name.EndsWith("Provider"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterType<AcceptHeaderParser>()
                .As<IHeaderParser<AcceptHeader>>()
                .InstancePerRequest();

            builder
                .RegisterType<RoutingService>()
                .As<IRoutingService>()
                .InstancePerRequest();

            builder
                .RegisterType<SettingsReader>()
                .As<ISettingsReader>()
                .InstancePerRequest();

            ////builder
            ////    .RegisterType<DefaultVendorNameProvider>()
            ////    .As<IVendorNameProvider>()
            ////    .InstancePerRequest();

            builder
                .RegisterType<RequestPropertiesAcceptHeaderStore>()
                .As<IAcceptHeaderStore>()
                .InstancePerRequest();

            builder
                .RegisterType<RequestIdProvider>()
                .As<IRequestIdProvider>()
                .InstancePerRequest();

            builder
                .RegisterType<HttpRequestMessageResolver>()
                .As<IHttpRequestMessageResolver>()
                .InstancePerRequest();

            builder
                .Register(b => new Log("Rocket.Routing"))
                .As<ILog>()
                .InstancePerRequest();
        }
    }
}
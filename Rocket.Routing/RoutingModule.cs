// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingModule.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

namespace Rocket.Routing
{
    public class RoutingModule : Module
    {
        private readonly HttpConfiguration _httpConfiguration;

        public RoutingModule(HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterHttpRequestMessage(_httpConfiguration);

            builder
                .RegisterType<HttpRequestMessage>();

            builder
                .RegisterType<MediaTypeHeaderParser>()
                .As<IHeaderParser<AcceptHeader>>()
                .InstancePerRequest();

            builder
                .RegisterType<SettingsReader>()
                .As<ISettingsReader>()
                .InstancePerRequest();

            builder
                .Register(c => new RequestIdProvider(c.Resolve<HttpRequestMessage>()))
                .As<IRequestIdProvider>()
                .InstancePerRequest();
        }
    }
}
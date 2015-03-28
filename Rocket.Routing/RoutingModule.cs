// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingModule.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Web;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

using Rocket.Core.Configuration;
using Rocket.Routing.Entities;
using Rocket.Routing.Extensions;
using Rocket.Routing.Http;
using Rocket.Routing.Providers;

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

            //builder
            //    .RegisterHttpRequestMessage(_httpConfiguration);

            //builder.RegisterInstance(_httpConfiguration).As<HttpConfiguration>();

            //builder
            //    .RegisterHttpRequestMessage(_httpConfiguration);

            //builder.RegisterHttpRequestMessage(
            //    GlobalConfiguration.Configuration);

           // builder.Register(c =>
           //            c.Resolve<HttpRequestMessage>()
           //             .GetConfiguration())
           //.As<HttpConfiguration>();


            //builder
            //    .RegisterType<HttpRequestMessage>();

            builder
                .RegisterType<AcceptHeaderParser>()
                .As<IHeaderParser<AcceptHeader>>()
                .InstancePerRequest();

            builder
                .RegisterType<SettingsReader>()
                .As<ISettingsReader>()
                .InstancePerRequest();

            builder
                .RegisterType<DefaultVendorNameProvider>()
                .As<IVendorNameProvider>()
                .InstancePerRequest();

            //builder
            //    .Register(c => new VersionComparer(
            //        c.Resolve<IHeaderParser<AcceptHeader>>(),
            //        c.Resolve<IRequestIdProvider>(),
            //        c.Resolve<HttpRequestMessage>()))
            //    .As<IVersionComparer>()
            //    .InstancePerRequest();

            builder
                .Register(
                c => new RequestPropertiesAcceptHeaderStore(
                    c.Resolve<IRequestIdProvider>(),
                    builder.CurrentRequest()))
                .As<IAcceptHeaderStore>()
                .InstancePerRequest();

            builder
                .Register(c => new RequestIdProvider(builder.CurrentRequest()))
                .As<IRequestIdProvider>()
                .InstancePerRequest();
        }
    }
}
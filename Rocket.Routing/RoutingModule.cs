// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingModule.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Autofac;

namespace Rocket.Routing
{
    public class RoutingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MediaTypeHeaderParser>()
                .As(typeof(IHeaderParser));

            builder
                .RegisterType<SettingsReader>()
                .As<ISettingsReader>();
        }
    }
}
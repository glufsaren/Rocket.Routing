// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapConfig.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the StructureMapConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;

using Rocket.Routing.IoC.StructureMap;
using Rocket.Routing.Services.Contracts;

using StructureMap;

namespace Routing
{
    public static class StructureMapConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new Container(
                expression =>
                {
                    expression
                        .For<IVendorNameService>()
                        .Use<DefaultVendorNameService>()
                        .Transient();

                    expression
                        .For<IRequestIdService>()
                        .Use<DefaultRequestIdService>()
                        .Transient();
                });

            config.DependencyResolver = new StructureMapResolver(container);
        }
    }
}
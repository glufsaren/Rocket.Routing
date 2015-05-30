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

using Autofac;
using Autofac.Integration.WebApi;

using Rocket.Routing.Services.Contracts;

namespace Routing
{
    public static class AutofacConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            var executingAssembly = Assembly.GetExecutingAssembly();

            builder
                .RegisterApiControllers(executingAssembly);

            builder
                .RegisterAssemblyTypes(executingAssembly)
                .Where(t => !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
                .InstancePerRequest();

            builder
                .RegisterType<DefaultVendorNameService>()
                .As<IVendorNameService>()
                .InstancePerRequest();

            builder
                .RegisterType<DefaultRequestIdService>()
                .As<IRequestIdService>()
                .InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
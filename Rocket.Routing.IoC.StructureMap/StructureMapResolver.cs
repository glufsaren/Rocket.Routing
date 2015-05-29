// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapResolver.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the StructureMapResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

using StructureMap;

namespace Rocket.Routing.IoC.StructureMap
{
    public class StructureMapResolver : StructureMapDependencyScope, IDependencyResolver, IHttpControllerActivator
    {
        private readonly IContainer _container;

        public StructureMapResolver(IContainer container)
            : base(container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
            _container.Inject(typeof(IHttpControllerActivator), this);
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(_container.GetNestedContainer());
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return _container.GetNestedContainer().GetInstance(controllerType) as IHttpController;
        }
    }
}
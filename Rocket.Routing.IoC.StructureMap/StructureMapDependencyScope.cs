// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapDependencyScope.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the StructureMapDependencyScope type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

using StructureMap;

namespace Rocket.Routing.IoC.StructureMap
{
    public class StructureMapDependencyScope : IDependencyScope
    {
        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Container = container;
        }

        public IContainer Container { get; private set; }

        public object GetService(Type serviceType)
        {
            if (Container == null)
            {
                throw new ObjectDisposedException(
                    "this", "This scope has already been disposed.");
            }

            return Container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (Container == null)
            {
                throw new ObjectDisposedException(
                    "this", "This scope has already been disposed.");
            }

            return Container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (Container != null)
            {
                Container.Dispose();
            }

            Container = null;
        }
    }
}
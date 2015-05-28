// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Initializer.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the Initializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Web.Http;

namespace Rocket.Routing
{
    internal class Initializer
    {
        public Initializer(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MessageHandlers
                .Add(new MessageHeadersHandler());

            var uri = new Uri(Assembly
                .GetExecutingAssembly().GetName().CodeBase);

            var path = Path.GetDirectoryName(uri.LocalPath);

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException(
                    "Path cannot be null or empty.");
            }

            var catalog = new DirectoryCatalog(path);

            var container = new CompositionContainer(catalog);

            container.ComposeParts(this);

            Bootstrapper.Configure(httpConfiguration);
        }

        [Import]
        public IBootstrapper Bootstrapper { private get; set; }
    }
}
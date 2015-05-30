// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using System.Threading;
using System.Web.Http;

namespace Rocket.Routing
{
    [Export]
    internal class Bootstrapper
    {
        private static Initializer initializer;

        public static void Initialize(HttpConfiguration httpConfiguration)
        {
            LazyInitializer.EnsureInitialized(
                ref initializer, () => new Initializer(httpConfiguration));
        }

        internal static void Reset()
        {
            initializer = null;
        }
    }
}
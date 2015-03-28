// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendorNameProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VendorNameProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Rocket.Routing.Providers;

namespace Routing
{
    internal sealed class VendorNameProvider : DefaultVendorNameProvider
    {
        public override string GetName()
        {
            return "rocket";
        }
    }
}
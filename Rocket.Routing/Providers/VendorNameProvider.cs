// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendorNameProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VendorNameProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Providers
{
    internal sealed class VendorNameProvider : IVendorNameProvider
    {
        public string Get()
        {
            return "acme";
        }
    }
}
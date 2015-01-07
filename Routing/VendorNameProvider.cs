﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendorNameProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VendorNameProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Rocket.Routing;

namespace Routing
{
    internal sealed class VendorNameProvider : IVendorNameProvider
    {
        public string Get()
        {
            return "rocket";
        }
    }
}
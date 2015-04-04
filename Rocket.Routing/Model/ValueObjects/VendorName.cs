// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendorName.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VendorName type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;

namespace Rocket.Routing.Model.ValueObjects
{
    internal class VendorName
    {
        public VendorName(string vendorName)
        {
            Value = CultureInfo.InvariantCulture.TextInfo
                .ToTitleCase((vendorName ?? string.Empty)
                .ToLower());
        }

        public string Value { get; private set; }
    }
}
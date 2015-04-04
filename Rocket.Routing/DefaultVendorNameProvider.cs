// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultVendorNameProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the DefaultVendorNameProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace Rocket.Routing
{
    [UsedImplicitly]
    public class DefaultVendorNameProvider : IVendorNameProvider
    {
        public const string DefaultVendorName = "acme";
        public const string CustomMediaTypePattern = @"^application\/(vnd\.#VENDOR_NAME#(\.[a-zA-Z0-9-]{2,20})*)?(\+?(?<contenttype>[a-zA-Z0-9-\.]*?));?(\sversion=(?<version>\d+(\.\d+)*);?)?$";

        public virtual string GetName()
        {
            return "rocket";
        }

        public virtual string GetPlaceHolder()
        {
            return "#VENDOR_NAME#";
        }

        public virtual string GetPattern()
        {
            return CustomMediaTypePattern;
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypeExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypeExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Rocket.Routing
{
    public static class MediaTypeExtensions
    {
        public static bool HasRequestId(this MediaType mediaType)
        {
            return mediaType != null && mediaType.RequestId != Guid.Empty;
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypeExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypeExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Rocket.Routing.Model.Entities;

namespace Rocket.Routing.Model
{
    internal static class MediaTypeExtensions
    {
        public static bool HasRequestId(this MediaType mediaType)
        {
            return mediaType != null && mediaType.RequestId != Guid.Empty;
        }
    }
}
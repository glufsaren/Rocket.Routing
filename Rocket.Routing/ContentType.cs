﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentType.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the ContentType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing
{
    public enum ContentType
    {
        Unspecified,

        /// <summary>
        /// Represents the MIME media type for JSON text.
        /// </summary>
        Json,

        Xml
    }
}
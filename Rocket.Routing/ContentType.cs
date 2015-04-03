// --------------------------------------------------------------------------------------------------------------------
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
        /// <summary>
        /// Unspecified media type format.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Represents the MIME media type for JSON content.
        /// </summary>
        Json,

        /// <summary>
        /// Represents the MIME media type for XML content.
        /// </summary>
        Xml
    }
}
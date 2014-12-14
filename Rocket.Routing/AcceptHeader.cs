// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeader.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing
{
    internal class AcceptHeader
    {
        public AcceptHeader()
        {
            RequestedVersion = 0;
            ContentType = ContentType.Json;
        }

        public double? RequestedVersion { get; set; }

        public ContentType ContentType { get; set; }
    }
}
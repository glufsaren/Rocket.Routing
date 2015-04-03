// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaType.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Rocket.Routing
{
    public class MediaType
    {
        public virtual double? RequestedVersion { get; set; }

        public virtual double ActualVersion { get; set; }

        public virtual ContentType ContentType { get; set; }

        public virtual Guid RequestId { get; set; }

        public virtual bool Matched { get; set; }
    }
}
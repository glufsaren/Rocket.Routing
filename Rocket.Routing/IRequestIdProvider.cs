// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestIdProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IRequestIdProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Rocket.Routing
{
    /// <summary>
    /// Used to provide the request id (correlation id) used to track requests and responses.
    /// </summary>
    public interface IRequestIdProvider
    {
        /// <summary>
        /// Gets a unique identifier.
        /// </summary>
        /// <returns>The GUID to use as request id.</returns>
        Guid Get();
    }
}
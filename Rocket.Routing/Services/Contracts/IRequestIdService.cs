// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestIdService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IRequestIdService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Rocket.Routing.Services.Contracts
{
    /// <summary>
    /// Used to provide the request id (correlation id) used to track requests and responses.
    /// </summary>
    public interface IRequestIdService
    {
        /// <summary>
        /// Gets a unique identifier.
        /// </summary>
        /// <returns>The GUID to use as request id.</returns>
        Guid Get();
    }
}
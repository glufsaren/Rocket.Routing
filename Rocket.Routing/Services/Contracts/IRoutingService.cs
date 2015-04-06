// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoutingService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IRoutingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Services.Contracts
{
    public interface IRoutingService
    {
        bool Match(
            string acceptHeaderValue,
            double version,
            bool isLatest);
    }
}
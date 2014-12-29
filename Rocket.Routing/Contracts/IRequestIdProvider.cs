// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequestIdProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IRequestIdProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Rocket.Routing.Contracts
{
    public interface IRequestIdProvider
    {
        Guid Get();
    }
}
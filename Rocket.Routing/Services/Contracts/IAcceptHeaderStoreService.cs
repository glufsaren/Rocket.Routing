// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAcceptHeaderStoreService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAcceptHeaderStoreService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Rocket.Routing.Model.Entities;
using Rocket.Routing.Model.ValueObjects;

namespace Rocket.Routing.Services.Contracts
{
    public interface IAcceptHeaderStoreService
    {
        void Set(AcceptHeader acceptHeader);

        MediaType Get();
    }
}
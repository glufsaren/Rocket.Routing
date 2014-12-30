// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAcceptHeaderStore.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAcceptHeaderStore type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Rocket.Routing.Entities;

namespace Rocket.Routing
{
    public interface IAcceptHeaderStore
    {
        void Set(AcceptHeader acceptHeader);
    }
}
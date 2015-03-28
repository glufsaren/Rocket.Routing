// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAcceptHeaderPatternProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAcceptHeaderPatternProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing
{
    public interface IAcceptHeaderPatternProvider
    {
        string Get();
    }
}
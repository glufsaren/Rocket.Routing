// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBootstrapper.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;

namespace Rocket.Routing
{
    public interface IBootstrapper
    {
        void Configure(HttpConfiguration configuration);
    }
}
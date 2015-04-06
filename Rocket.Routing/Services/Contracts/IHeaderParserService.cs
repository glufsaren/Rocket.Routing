// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeaderParserService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IHeaderParserService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Services.Contracts
{
    public interface IHeaderParserService<out T>
    {
        T Parse(string headerValue);
    }
}
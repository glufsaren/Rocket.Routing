// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeaderParser.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IHeaderParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Contracts
{
    public interface IHeaderParser<out T>
    {
        T Parse(string headerValue);
    }
}
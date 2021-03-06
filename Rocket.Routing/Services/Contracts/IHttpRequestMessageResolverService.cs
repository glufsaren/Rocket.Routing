﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHttpRequestMessageResolverService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IHttpRequestMessageResolverService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

namespace Rocket.Routing.Services.Contracts
{
    public interface IHttpRequestMessageResolverService
    {
        HttpRequestMessage Current();
    }
}
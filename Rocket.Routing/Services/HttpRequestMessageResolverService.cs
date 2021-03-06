﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestMessageResolverService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the HttpRequestMessageResolverService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Web;

using Rocket.Routing.Services.Contracts;

namespace Rocket.Routing.Services
{
    public sealed class HttpRequestMessageResolverService : IHttpRequestMessageResolverService
    {
        public HttpRequestMessage Current()
        {
            return HttpContext.Current
                .Items["MS_HttpRequestMessage"] as HttpRequestMessage;
        }
    }
}
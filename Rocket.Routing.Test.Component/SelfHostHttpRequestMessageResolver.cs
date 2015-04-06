// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelfHostHttpRequestMessageResolver.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the SelfHostHttpRequestMessageResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

using Rocket.Routing.Services.Contracts;
using Rocket.Test;

namespace Rocket.Routing.Test.Component
{
    public class SelfHostHttpRequestMessageResolver : IHttpRequestMessageResolverService
    {
        private readonly HttpServerHost _httpServerHost;

        public SelfHostHttpRequestMessageResolver(HttpServerHost httpServerHost)
        {
            _httpServerHost = httpServerHost;
        }

        public HttpRequestMessage Current()
        {
            return _httpServerHost.CurrentMessage;
        }
    }
}
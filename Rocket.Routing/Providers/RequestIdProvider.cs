// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestIdProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestIdProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

namespace Rocket.Routing.Providers
{
    public sealed class RequestIdProvider : IRequestIdProvider
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public RequestIdProvider(
            HttpRequestMessage httpRequestMessage)
        {
            _httpRequestMessage = httpRequestMessage;
        }

        public Guid Get()
        {
            return _httpRequestMessage.GetCorrelationId();
        }
    }
}
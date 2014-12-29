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
using System.Web;

using Rocket.Routing.Contracts;

namespace Rocket.Routing
{
    public sealed class RequestIdProvider : IRequestIdProvider
    {
        private HttpRequestMessage _httpRequestMessage;

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
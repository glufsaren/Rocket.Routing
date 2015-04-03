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

using JetBrains.Annotations;

namespace Rocket.Routing
{
    /// <inheritdoc/>
    [UsedImplicitly]
    internal sealed class RequestIdProvider : IRequestIdProvider
    {
        private readonly IHttpRequestMessageResolver _httpRequestMessageResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestIdProvider"/> class.
        /// </summary>
        /// <param name="httpRequestMessageResolver">The HTTP request message resolver.</param>
        public RequestIdProvider(
            IHttpRequestMessageResolver httpRequestMessageResolver)
        {
            _httpRequestMessageResolver = httpRequestMessageResolver;
        }

        /// <inheritdoc/>
        public Guid Get()
        {
            var httpRequestMessage =
                _httpRequestMessageResolver.Current();

            return httpRequestMessage.GetCorrelationId();
        }
    }
}
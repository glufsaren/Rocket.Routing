// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestIdService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestIdService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using JetBrains.Annotations;

using Rocket.Routing.Services.Contracts;

namespace Rocket.Routing.Services
{
    /// <inheritdoc/>
    [UsedImplicitly]
    public sealed class RequestIdService : IRequestIdService
    {
        private readonly IHttpRequestMessageResolverService _httpRequestMessageResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestIdService"/> class.
        /// </summary>
        /// <param name="httpRequestMessageResolver">The HTTP request message resolver.</param>
        public RequestIdService(
            IHttpRequestMessageResolverService httpRequestMessageResolver)
        {
            _httpRequestMessageResolver = httpRequestMessageResolver;
        }

        /// <inheritdoc/>
        public Guid Get()
        {
            var httpRequestMessage =
                _httpRequestMessageResolver.Current();

            return httpRequestMessage != null
                        ? httpRequestMessage.GetCorrelationId()
                        : Guid.Empty;
        }
    }
}
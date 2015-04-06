// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestPropertiesAcceptHeaderStoreService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestPropertiesAcceptHeaderStoreService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

using Rocket.Routing.Model.Entities;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services.Contracts;

namespace Rocket.Routing.Services
{
    [UsedImplicitly]
    public sealed class RequestPropertiesAcceptHeaderStoreService : IAcceptHeaderStoreService
    {
        private readonly IRequestIdService _requestIdService;
        private readonly IHttpRequestMessageResolverService _httpRequestMessageResolver;

        public RequestPropertiesAcceptHeaderStoreService(
                        IRequestIdService requestIdService,
                        IHttpRequestMessageResolverService httpRequestMessageResolver)
        {
            _requestIdService = requestIdService;
            _httpRequestMessageResolver = httpRequestMessageResolver;
        }

        public void Set(AcceptHeader acceptHeader)
        {
            var httpRequestMessage =
                _httpRequestMessageResolver.Current();

            var mediaTypeProperties =
                new RequestPropertiesMediaType(httpRequestMessage);

            if (acceptHeader.Matches)
            {
                mediaTypeProperties.RequestedVersion = acceptHeader.RequestedVersion;
                mediaTypeProperties.ContentType = acceptHeader.ContentType;
                mediaTypeProperties.ActualVersion = acceptHeader.ActualVersion;
                mediaTypeProperties.Matched = true;
            }

            mediaTypeProperties.RequestId = _requestIdService.Get();
        }

        public MediaType Get()
        {
            var httpRequestMessage =
                _httpRequestMessageResolver.Current();

            return new RequestPropertiesMediaType(httpRequestMessage);
        }
    }
}
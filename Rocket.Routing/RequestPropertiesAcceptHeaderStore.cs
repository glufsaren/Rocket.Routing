// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestPropertiesAcceptHeaderStore.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestPropertiesAcceptHeaderStore type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

using JetBrains.Annotations;

namespace Rocket.Routing
{
    [UsedImplicitly]
    public class RequestPropertiesAcceptHeaderStore : IAcceptHeaderStore
    {
        private readonly IRequestIdProvider _requestIdProvider;

        private readonly IHttpRequestMessageResolver _httpRequestMessageResolver;

        public RequestPropertiesAcceptHeaderStore(
                        IRequestIdProvider requestIdProvider,
                        IHttpRequestMessageResolver httpRequestMessageResolver)
        {
            _requestIdProvider = requestIdProvider;
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

            mediaTypeProperties.RequestId = _requestIdProvider.Get();
        }

        public MediaType Get()
        {
            var httpRequestMessage =
                _httpRequestMessageResolver.Current();

            return new RequestPropertiesMediaType(httpRequestMessage);
        }
    }
}
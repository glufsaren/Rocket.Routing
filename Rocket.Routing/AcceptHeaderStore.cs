// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderStore.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderStore type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

using Rocket.Routing.Contracts;
using Rocket.Routing.Entities;

namespace Rocket.Routing
{
    public class AcceptHeaderStore : IAcceptHeaderStore
    {
        private readonly IRequestIdProvider _requestIdProvider;
        private readonly HttpRequestMessage _httpRequestMessage;

        public AcceptHeaderStore(
            IRequestIdProvider requestIdProvider,
            HttpRequestMessage httpRequestMessage)
        {
            _requestIdProvider = requestIdProvider;
            _httpRequestMessage = httpRequestMessage;
        }

        public void Set(AcceptHeader acceptHeader)
        {
            var mediaTypeProperties = 
                new MediaTypeProperties(_httpRequestMessage);

            if (acceptHeader.Matches)
            {
                mediaTypeProperties.RequestedVersion = acceptHeader.RequestedVersion;
                mediaTypeProperties.ContentType = acceptHeader.ContentType;
                mediaTypeProperties.ActualVersion = acceptHeader.ActualVersion;
                mediaTypeProperties.Matched = true;
            }

            mediaTypeProperties.RequestId = _requestIdProvider.Get();
        }
    }
}
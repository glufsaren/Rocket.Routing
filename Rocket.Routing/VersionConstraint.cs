// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionConstraint.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VersionConstraint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

using Rocket.Routing.Contracts;
using Rocket.Routing.Entities;
using Rocket.Web.Extensions;

namespace Rocket.Routing
{
    internal sealed class VersionConstraint : IHttpRouteConstraint
    {
        private const string AcceptHeader = "accept";

        private readonly bool _isLatest;
        private readonly double _version;

        public VersionConstraint(
            double version, bool isLatest)
        {
            _isLatest = isLatest;
            _version = version;
        }

        public bool Match(
            HttpRequestMessage httpRequestMessage,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            if (routeDirection != HttpRouteDirection.UriResolution)
            {
                return false;
            }

            return Match(
                httpRequestMessage,
                GetHeaderParser(httpRequestMessage));
        }

        internal bool Match(
            HttpRequestMessage httpRequestMessage,
            IHeaderParser<AcceptHeader> headerParser)
        {
            var acceptHeaderValue =
                httpRequestMessage.TryGetHeader(AcceptHeader);

            var acceptHeader =
                headerParser.Parse(acceptHeaderValue);

            var match = acceptHeader
                .MatchHeaderVersion(_version, _isLatest);

            BindMediaTypeInformationToRequest(
                httpRequestMessage, match, acceptHeader);

            return match;
        }

        private static IHeaderParser<AcceptHeader> GetHeaderParser(
            HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage
                .GetService<IHeaderParser<AcceptHeader>>();
        }

        private static IMediaTypeStore GetMediaTypeStore(
            HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage
                .GetService<IMediaTypeStore>();
        }

        private static IRequestIdProvider Get(
            HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage
                .GetService<IRequestIdProvider>();
        }

        private void BindMediaTypeInformationToRequest(
            HttpRequestMessage httpRequestMessage,
            bool isMatch,
            AcceptHeader mediaType)
        {
            var mediaTypeProperties = new MediaTypeProperties(httpRequestMessage);

            if (isMatch)
            {
                mediaTypeProperties.RequestedVersion = mediaType.RequestedVersion;
                mediaTypeProperties.ContentType = mediaType.ContentType;
                mediaTypeProperties.ActualVersion = _version;
            }

            mediaTypeProperties.RequestId = Get(httpRequestMessage).Get();
        }
    }

    internal interface IMediaTypeStore
    {
        //void Set();
    }

    public class MediaTypeStore : IMediaTypeStore
    {
        private IRequestIdProvider _requestIdProvider;

        public MediaTypeStore(
            IRequestIdProvider requestIdProvider)
        {
            _requestIdProvider = requestIdProvider;
        }
    }
}
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

using Rocket.Routing.Extensions;

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

            bool isMatch;
            AcceptHeader mediaType;

            try
            {
                mediaType = ParseAcceptHeader(
                    httpRequestMessage,
                    ResolveMediaTypeHeaderParser(httpRequestMessage));

                isMatch = MatchHeaderVersion(mediaType);
            }
            catch
            {
                return false;
            }

            BindMediaTypeInformationToRequest(
                httpRequestMessage, isMatch, mediaType);

            return isMatch;
        }

        internal static AcceptHeader ParseAcceptHeader(
            HttpRequestMessage httpRequestMessage,
            IHeaderParser<AcceptHeader> headerParser)
        {
            var acceptHeader =
                httpRequestMessage.TryGetHeader(AcceptHeader);

            var mediaType = new AcceptHeader();

            return acceptHeader != null
                ? headerParser.Parse(acceptHeader)
                : mediaType;
        }

        internal bool MatchHeaderVersion(AcceptHeader acceptHeader)
        {
            return MatchesLatestVersion(acceptHeader)
                   || MatchesVersion(acceptHeader);
        }

        private static MediaTypeHeaderParser ResolveMediaTypeHeaderParser(HttpRequestMessage httpRequestMessage)
        {
            return (MediaTypeHeaderParser)httpRequestMessage
                .GetService<IHeaderParser<AcceptHeader>>();
        }

        private static IRequestIdProvider ResolveRequestIdProvider(HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage.GetService<IRequestIdProvider>();
        }

        private void BindMediaTypeInformationToRequest(
            HttpRequestMessage httpRequestMessage, bool isMatch, AcceptHeader mediaType)
        {
            var mediaTypeProperties = new MediaTypeProperties(httpRequestMessage);

            if (isMatch)
            {
                mediaTypeProperties.RequestedVersion = mediaType.RequestedVersion;
                mediaTypeProperties.ContentType = mediaType.ContentType;
                mediaTypeProperties.ActualVersion = _version;
            }

            var requestIdProvider =
                ResolveRequestIdProvider(httpRequestMessage);

            mediaTypeProperties.RequestId = requestIdProvider.Get();
        }

        private bool MatchesVersion(AcceptHeader acceptHeader)
        {
            return acceptHeader.RequestedVersion.HasValue
                && acceptHeader.RequestedVersion.Value == _version;
        }

        private bool MatchesLatestVersion(AcceptHeader acceptHeader)
        {
            return !acceptHeader.RequestedVersion.HasValue
                && _isLatest;
        }
    }
}
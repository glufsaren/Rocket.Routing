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
            HttpRequestMessage request,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            if (routeDirection != HttpRouteDirection.UriResolution)
            {
                return false;
            }

            try
            {
                var mediaType = ParseAcceptHeader(
                    ResolveMediaTypeHeaderParser(request), request);

                bool isMatch = MatchHeaderVersion(mediaType);

                if (isMatch)
                {
                    mediaType.BindRequest(request);
                }

                return isMatch;
            }
            catch
            {
                return false;
            }
        }

        internal static MediaType ParseAcceptHeader(
            IHeaderParser headerParser, HttpRequestMessage httpRequestMessage)
        {
            var acceptHeader =
                httpRequestMessage.TryGetHeader(AcceptHeader);

            return acceptHeader != null
                ? headerParser.Parse(acceptHeader)
                : new MediaType();
        }

        internal bool MatchHeaderVersion(MediaType mediaType)
        {
            var matchesVersion = MatchesLatestVersion(mediaType)
                              || MatchesVersion(mediaType);

            if (matchesVersion)
            {
                mediaType.ActualVersion = _version;
            }

            return matchesVersion;
        }

        private static MediaTypeHeaderParser ResolveMediaTypeHeaderParser(HttpRequestMessage requestMessage)
        {
            return (MediaTypeHeaderParser)requestMessage
                                        .GetDependencyScope()
                                        .GetService(typeof(IHeaderParser));

            //return new MediaTypeHeaderParser(new SettingsReader());
        }

        private bool MatchesVersion(MediaType mediaType)
        {
            return mediaType.RequestedVersion.HasValue
                && mediaType.RequestedVersion.Value == _version;
        }

        private bool MatchesLatestVersion(MediaType mediaType)
        {
            return !mediaType.RequestedVersion.HasValue
                && _isLatest;
        }
    }
}
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
                GetHeaderParser(httpRequestMessage),
                GetAcceptHeaderStore(httpRequestMessage));
        }

        internal bool Match(
            HttpRequestMessage httpRequestMessage,
            IHeaderParser<AcceptHeader> headerParser,
            IAcceptHeaderStore acceptHeaderStore)
        {
            var acceptHeaderValue =
                httpRequestMessage.TryGetHeader(AcceptHeader);

            var acceptHeader =
                headerParser.Parse(acceptHeaderValue);

            if (acceptHeader == null)
            {
                return false;
            }

            acceptHeader
                .MatchHeaderVersion(_version, _isLatest);

            acceptHeaderStore.Set(acceptHeader);

            return acceptHeader.Matches;
        }

        private static IHeaderParser<AcceptHeader> GetHeaderParser(
            HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage
                .GetService<IHeaderParser<AcceptHeader>>();
        }

        private static IAcceptHeaderStore GetAcceptHeaderStore(
            HttpRequestMessage httpRequestMessage)
        {
            return httpRequestMessage
                .GetService<IAcceptHeaderStore>();
        }
    }
}
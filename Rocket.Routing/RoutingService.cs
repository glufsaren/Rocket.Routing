// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace Rocket.Routing
{
    [UsedImplicitly]
    public class RoutingService : IRoutingService
    {
        private readonly IHeaderParser<AcceptHeader> _headerParser;
        private readonly IAcceptHeaderStore _acceptHeaderStore;

        public RoutingService(
            IAcceptHeaderStore acceptHeaderStore,
            IHeaderParser<AcceptHeader> headerParser)
        {
            _acceptHeaderStore = acceptHeaderStore;
            _headerParser = headerParser;
        }

        public bool Match(
                    string acceptHeaderValue,
                    double version,
                    bool isLatest)
        {
            var acceptHeader =
                CreateAcceptHeader(acceptHeaderValue);

            if (acceptHeader == null)
            {
                return false;
            }

            acceptHeader
                .MatchHeaderVersion(version, isLatest);

            _acceptHeaderStore.Set(acceptHeader);

            return acceptHeader.Matches;
        }

        private AcceptHeader CreateAcceptHeader(string acceptHeaderValue)
        {
            return !string.IsNullOrWhiteSpace(acceptHeaderValue)
                       ? _headerParser.Parse(acceptHeaderValue)
                       : AcceptHeaderFactory.Default();
        }
    }
}
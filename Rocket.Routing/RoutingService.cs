// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http.Headers;

using Rocket.Routing.Entities;
using Rocket.Web.Extensions;

namespace Rocket.Routing
{
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
                _headerParser.Parse(acceptHeaderValue);

            if (acceptHeader == null)
            {
                return false;
            }

            acceptHeader
                .MatchHeaderVersion(version, isLatest);

            _acceptHeaderStore.Set(acceptHeader);

            return acceptHeader.Matches;
        }
    }
}
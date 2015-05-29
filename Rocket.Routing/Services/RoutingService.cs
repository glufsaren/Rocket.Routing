// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Rocket.Core.Diagnostics;
using Rocket.Routing.Model.Factories;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services.Contracts;

namespace Rocket.Routing.Services
{
    public sealed class RoutingService : IRoutingService
    {
        private readonly IHeaderParserService<AcceptHeader> _headerParserService;
        private readonly IAcceptHeaderStoreService _acceptHeaderStoreService;
        private readonly ILog _log;

        public RoutingService(
            IAcceptHeaderStoreService acceptHeaderStoreService,
            IHeaderParserService<AcceptHeader> headerParserService,
            ILog log)
        {
            _acceptHeaderStoreService = acceptHeaderStoreService;
            _headerParserService = headerParserService;
            _log = log;
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

            try
            {
                acceptHeader
                    .MatchHeaderVersion(version, isLatest);

                _acceptHeaderStoreService.Set(acceptHeader);

                return acceptHeader.Matches;
            }
            catch (Exception ex)
            {
                _log.Error("Error while routing request.", ex);

                return false;
            }
        }

        private AcceptHeader CreateAcceptHeader(string acceptHeaderValue)
        {
            return !string.IsNullOrWhiteSpace(acceptHeaderValue)
                       ? _headerParserService.Parse(acceptHeaderValue)
                       : AcceptHeaderFactory.Default();
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderParserService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderParserService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Text.RegularExpressions;

using Rocket.Core.Diagnostics;
using Rocket.Routing.Model.Factories;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services.Contracts;

namespace Rocket.Routing.Services
{
    public sealed class AcceptHeaderParserService : IHeaderParserService<AcceptHeader>
    {
        private readonly IAcceptHeaderPatternService _acceptHeaderPatternService;
        private readonly ILog _log;

        public AcceptHeaderParserService(
            IAcceptHeaderPatternService acceptHeaderPatternService,
            ILog log)
        {
            _acceptHeaderPatternService = acceptHeaderPatternService;
            _log = log;
        }

        public AcceptHeader Parse(string acceptHeader)
        {
            if (string.IsNullOrWhiteSpace(acceptHeader))
            {
                _log.Debug("No accept header specified.");
                return null;
            }

            var pattern = GetPattern();
            _log.DebugFormat(
                    "Using pattern: {0}",
                    pattern);

            if (string.IsNullOrWhiteSpace(pattern))
            {
                return null;
            }

            var match = Match(
                acceptHeader, pattern);

            return match != null
                ? AcceptHeaderFactory.Create(match)
                : null;
        }

        private static Match Match(string acceptHeader, string pattern)
        {
            MatchCollection matches = Regex.Matches(
                acceptHeader ?? string.Empty, pattern);

            return matches
                    .Cast<Match>()
                    .FirstOrDefault();
        }

        private string GetPattern()
        {
            return _acceptHeaderPatternService.Get() ?? string.Empty;
        }
    }
}
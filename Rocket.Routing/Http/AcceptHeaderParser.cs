// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderParser.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

using Rocket.Core.Extensions;
using Rocket.Routing.Entities;

namespace Rocket.Routing.Http
{
    // TODO: Felhantering
    internal sealed class AcceptHeaderParser : IHeaderParser<AcceptHeader>
    {
        private readonly IAcceptHeaderPatternProvider _acceptHeaderPatternProvider;

        public AcceptHeaderParser(
            IAcceptHeaderPatternProvider acceptHeaderPatternProvider)
        {
            _acceptHeaderPatternProvider = acceptHeaderPatternProvider;
        }

        public AcceptHeader Parse(string acceptHeader)
        {
            EnsureAcceptHeaderSpecified(acceptHeader);

            var matchCollection =
                MatchCustomMediaTypeHeader(acceptHeader);

            return CreateMediaTypeFromMatches(matchCollection);
        }

        private static AcceptHeader CreateMediaTypeFromMatches(IEnumerable matchCollection)
        {
            var acceptHeader = new AcceptHeader();

            if (matchCollection == null)
            {
                return acceptHeader;
            }

            var match = matchCollection
                            .Cast<Match>()
                            .FirstOrDefault();

            if (match == null)
            {
                return acceptHeader;
            }

            acceptHeader.RequestedVersion = ParseVersion(match);
            acceptHeader.ContentType = ParseContentType(match);

            return acceptHeader;
        }

        private static double? ParseVersion(Match match)
        {
            var value = match.Groups["version"].Value;

            return !string.IsNullOrWhiteSpace(value)
                    ? value.ToDouble()
                    : (double?)null;
        }

        private static ContentType ParseContentType(Match match)
        {
            var value = match.Groups["contenttype"].Value;

            if (string.IsNullOrWhiteSpace(value))
            {
                return ContentType.Json;
            }

            ContentType contentType;

            if (!Enum.TryParse(value, true, out contentType))
            {
                throw new InvalidOperationException();
            }

            return contentType;
        }

        private static void EnsureAcceptHeaderSpecified(string acceptHeader)
        {
            if (string.IsNullOrWhiteSpace(acceptHeader))
            {
                throw new ArgumentNullException("acceptHeader");
            }
        }

        private MatchCollection MatchCustomMediaTypeHeader(string acceptHeader)
        {
            var pattern = _acceptHeaderPatternProvider.Get();

            MatchCollection matches =
                Regex.Matches(acceptHeader, pattern);

            return !matches.IsNullOrEmpty() ? matches : null;
        }
    }
}
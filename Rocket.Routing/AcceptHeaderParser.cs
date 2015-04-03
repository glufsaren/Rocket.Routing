// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderParser.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rocket.Routing
{
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
            var match = Match(
                acceptHeader, GetPattern());

            return match != null
                ? AcceptHeaderFactory.Create(match)
                : null;
        }

        private static Match Match(string acceptHeader, string pattern)
        {
            MatchCollection matches =
                Regex.Matches(acceptHeader, pattern);

            return matches
                    .Cast<Match>()
                    .FirstOrDefault();
        }

        private string GetPattern()
        {
            return _acceptHeaderPatternProvider.Get() ?? string.Empty;
        }
    }
}
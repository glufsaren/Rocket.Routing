// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypeHeaderParser.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypeHeaderParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

using Rocket.Routing.Extensions;

namespace Rocket.Routing
{
    internal sealed class MediaTypeHeaderParser : IHeaderParser
    {
        private const string CustomMediaTypePattern = @"^application\/(vnd\.acme(\.[a-zA-Z0-9-]{2,20})*)?(\+?(?<contenttype>[a-zA-Z0-9-\.]*?));?(\sversion=(?<version>\d+(\.\d+)*);?)?$";

        private readonly ISettingsReader _settingsReader;

        public MediaTypeHeaderParser(ISettingsReader settingsReader)
        {
            _settingsReader = settingsReader;
        }

        public MediaType Parse(string acceptHeader)
        {
            EnsureAcceptHeaderSpecified(acceptHeader);

            var matchCollection =
                MatchCustomMediaTypeHeader(acceptHeader);

            return CreateMediaTypeFromMatches(matchCollection);
        }

        private static MediaType CreateMediaTypeFromMatches(IEnumerable matchCollection)
        {
            var messageMetaData = new MediaType();

            if (matchCollection == null)
            {
                return messageMetaData;
            }

            var match = matchCollection
                            .Cast<Match>()
                            .FirstOrDefault();

            if (match == null)
            {
                return messageMetaData;
            }

            messageMetaData.RequestedVersion = ParseVersion(match);
            messageMetaData.ContentType = ParseContentType(match);

            return messageMetaData;
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
            MatchCollection matches =
                Regex.Matches(acceptHeader, GetPattern(CustomMediaTypePattern));

            return !matches.IsNullOrEmpty() ? matches : null;
        }

        private string GetPattern(string fallbackPattern)
        {
            var pattern = _settingsReader
                .GetAppSetting<string>("MediaTypePattern");

            return string.IsNullOrWhiteSpace(pattern)
                    ? fallbackPattern
                    : pattern;
        }
    }
}
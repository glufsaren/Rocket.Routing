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
using Rocket.Routing.Providers;

namespace Rocket.Routing.Http
{
    // TODO: Felhantering
    internal sealed class AcceptHeaderParser : IHeaderParser<AcceptHeader>
    {
        private const string CustomMediaTypePattern = @"^application\/(vnd\.#VENDOR_NAME#(\.[a-zA-Z0-9-]{2,20})*)?(\+?(?<contenttype>[a-zA-Z0-9-\.]*?));?(\sversion=(?<version>\d+(\.\d+)*);?)?$";

        private readonly ISettingsReader _settingsReader;
        private readonly IVendorNameProvider _vendorNameProvider;

        public AcceptHeaderParser(
            ISettingsReader settingsReader,
            IVendorNameProvider vendorNameProvider)
        {
            _settingsReader = settingsReader;
            _vendorNameProvider = vendorNameProvider;
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
            MatchCollection matches =
                Regex.Matches(acceptHeader, GetPattern(CustomMediaTypePattern));

            return !matches.IsNullOrEmpty() ? matches : null;
        }

        private string GetPattern(string fallbackPattern)
        {
            var pattern = _settingsReader
                .GetAppSetting<string>("MediaTypePattern");

            var matchPattern = string.IsNullOrWhiteSpace(pattern)
                    ? fallbackPattern
                    : pattern;

            return InsertVendorName(matchPattern);
        }

        private string InsertVendorName(string matchPattern)
        {
            var vendorName = _vendorNameProvider.Get();

            vendorName = !string.IsNullOrWhiteSpace(vendorName)
                ? vendorName
                : Constants.DefaultVendorName;

            return matchPattern
                .Replace("#VENDOR_NAME#", vendorName.ToLower());
        }
    }
}
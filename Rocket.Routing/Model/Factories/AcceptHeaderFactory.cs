// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderFactory.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

using Rocket.Core.Extensions;
using Rocket.Routing.Model.ValueObjects;

namespace Rocket.Routing.Model.Factories
{
    internal static class AcceptHeaderFactory
    {
        public static AcceptHeader Create(Match match)
        {
            return new AcceptHeader(
                        ParseContentType(match),
                        ParseVersion(match));
        }

        public static AcceptHeader Default()
        {
            return new AcceptHeader(
                ContentType.Json, null);
        }

        private static double? ParseVersion(Match match)
        {
            var value = match.GetValue("version");

            return !string.IsNullOrWhiteSpace(value)
                    ? value.ToDouble()
                    : default(double?);
        }

        private static ContentType ParseContentType(Match match)
        {
            var value = match.GetValue("contenttype");

            ContentType contentType;

            return !Enum.TryParse(value, true, out contentType)
                            ? ContentType.Json
                            : contentType;
        }
    }
}
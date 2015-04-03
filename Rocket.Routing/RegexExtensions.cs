// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RegexExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace Rocket.Routing
{
    internal static class RegexExtensions
    {
        public static string GetValue(this Match match, string key)
        {
            var group = match.Groups[key];

            if (group == null)
            {
                return string.Empty;
            }

            var value = group.Value;

            return !string.IsNullOrWhiteSpace(value)
                    ? value
                    : string.Empty;
        }
    }
}
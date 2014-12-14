// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the StringExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;

namespace Rocket.Routing.Extensions
{
    public static class StringExtensions
    {
        public static double ToDouble(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            double result;

            double.TryParse(
                        value,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out result);

            return result;
        }
    }
}
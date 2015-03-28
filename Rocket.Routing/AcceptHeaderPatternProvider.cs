// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderPatternProvider.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderPatternProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace Rocket.Routing
{
    [UsedImplicitly]
    internal sealed class AcceptHeaderPatternProvider : IAcceptHeaderPatternProvider
    {
        private const string DefaultVendorName = "acme";

        private readonly IVendorNameProvider _vendorNameProvider;

        public AcceptHeaderPatternProvider(
            IVendorNameProvider vendorNameProvider)
        {
            _vendorNameProvider = vendorNameProvider;
        }

        public string Get()
        {
            var vendorName = GetVendorName();

            var vendorNamePlaceHolder =
                _vendorNameProvider.GetPlaceHolder();

            var matchPattern =
                _vendorNameProvider.GetPattern();

            if (PlaceholderIsSpecified(vendorNamePlaceHolder) &&
                PatternHasPlaceholder(matchPattern, vendorNamePlaceHolder))
            {
                return matchPattern.Replace(
                    vendorNamePlaceHolder, vendorName.ToLower());
            }

            return matchPattern;
        }

        private static bool PatternHasPlaceholder(
            string matchPattern, string vendorNamePlaceHolder)
        {
            return matchPattern.IndexOf(
                vendorNamePlaceHolder, System.StringComparison.Ordinal) > -1;
        }

        private static bool PlaceholderIsSpecified(string vendorNamePlaceHolder)
        {
            return !string.IsNullOrWhiteSpace(vendorNamePlaceHolder);
        }

        private string GetVendorName()
        {
            var vendorName =
                _vendorNameProvider.GetName();

            return !string.IsNullOrWhiteSpace(vendorName)
                                ? vendorName
                                : DefaultVendorName;
        }
    }
}
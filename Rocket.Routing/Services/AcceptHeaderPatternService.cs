// --------------------------------------------------------------------------------------------------------------------
// <copyright file="acceptHeaderPatternService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the acceptHeaderPatternService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Rocket.Routing.Services.Contracts;

namespace Rocket.Routing.Services
{
    public sealed class AcceptHeaderPatternService : IAcceptHeaderPatternService
    {
        private readonly IVendorNameService _vendorNameService;

        public AcceptHeaderPatternService(
            IVendorNameService vendorNameService)
        {
            _vendorNameService = vendorNameService;
        }

        public string Get()
        {
            var vendorNamePlaceHolder =
                _vendorNameService.GetPlaceHolder();

            var matchPattern = GetMatchPattern();

            if (PlaceholderIsSpecified(vendorNamePlaceHolder) &&
                PatternHasPlaceholder(matchPattern, vendorNamePlaceHolder))
            {
                return matchPattern.Replace(
                    vendorNamePlaceHolder, GetVendorName().ToLower());
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
                _vendorNameService.GetName();

            return !string.IsNullOrWhiteSpace(vendorName)
                                ? vendorName
                                : DefaultVendorNameService.DefaultVendorName;
        }

        private string GetMatchPattern()
        {
            var matchPattern =
                _vendorNameService.GetPattern();

            return !string.IsNullOrWhiteSpace(matchPattern)
                                ? matchPattern
                                : DefaultVendorNameService.CustomMediaTypePattern;
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeader.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Entities
{
    internal class AcceptHeader
    {
        public AcceptHeader()
        {
            RequestedVersion = 0;
            ContentType = ContentType.Json;
        }

        public double? RequestedVersion { get; set; }

        public ContentType ContentType { get; set; }

        internal bool MatchHeaderVersion(double version, bool isLatest)
        {
            return MatchesLatestVersion(isLatest)
                   || MatchesVersion(version);
        }

        private bool MatchesVersion(double version)
        {
            return RequestedVersion.HasValue
                && RequestedVersion.Value == version;
        }

        private bool MatchesLatestVersion(bool isLatest)
        {
            return !RequestedVersion.HasValue && isLatest;
        }
    }
}
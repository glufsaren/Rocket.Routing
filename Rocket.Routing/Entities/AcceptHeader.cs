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
    public class AcceptHeader
    {
        public AcceptHeader()
        {
            RequestedVersion = 0;
            ContentType = ContentType.Json;
        }

        public ContentType ContentType { get; set; }

        public double? RequestedVersion { get; set; }

        public double ActualVersion { get; private set; }

        public bool Matches { get; private set; }

        public void MatchHeaderVersion(double version, bool isLatest)
        {
            Matches = MatchesLatestVersion(isLatest)
                   || MatchesVersion(version);

            if (Matches)
            {
                ActualVersion = version;
            }
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
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeader.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Model.ValueObjects
{
    public class AcceptHeader
    {
        public AcceptHeader(ContentType contentType, double? requestedVersion)
        {
            ContentType = contentType;
            RequestedVersion = requestedVersion;
        }

        public double ActualVersion { get; private set; }

        public ContentType ContentType { get; private set; }

        public bool Matches { get; private set; }

        public double? RequestedVersion { get; private set; }

        public void MatchHeaderVersion(double version, bool isLatest)
        {
            Matches = MatchesLatestVersion(isLatest)
                   || MatchesVersion(version);

            if (Matches)
            {
                ActualVersion = version;
            }
        }

        private bool MatchesLatestVersion(bool isLatest)
        {
            return !RequestedVersion.HasValue && isLatest;
        }

        private bool MatchesVersion(double version)
        {
            return RequestedVersion.HasValue
                && RequestedVersion.Value == version;
        }
    }
}
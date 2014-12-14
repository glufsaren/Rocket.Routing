// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaType.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using Rocket.Routing.Extensions;

namespace Rocket.Routing
{
    internal sealed class MediaType
    {
        private const string RequestedVersionKey = "VersionConstraint_RequestedVersion";
        private const string ActualVersionKey = "VersionConstraint_ActualVersion";
        private const string ContentTypeKey = "VersionConstraint_ContentType";
        private const string RequestIdKey = "VersionConstraint_RequestId";

        public MediaType()
        {
            ContentType = ContentType.Json;
            RequestedVersion = null;
        }

        public double? RequestedVersion { get; set; }

        public double ActualVersion { get; set; }

        public ContentType ContentType { get; set; }

        public Guid RequestId { get; set; }

        public static MediaType FromRequest(HttpRequestMessage request)
        {
            return new MediaType
                       {
                           RequestedVersion = request.GetProperty<double?>(RequestedVersionKey),
                           ActualVersion = request.GetProperty<double>(ActualVersionKey),
                           ContentType = request.GetProperty<ContentType>(ContentTypeKey),
                           RequestId = request.GetProperty<Guid>(RequestIdKey),
                       };
        }

        public void BindRequest(HttpRequestMessage request)
        {
            request.AddOrUpdateProperty(
                RequestedVersionKey, RequestedVersion);

            request.AddOrUpdateProperty(
                ActualVersionKey, ActualVersion);

            request.AddOrUpdateProperty(
                ContentTypeKey, ContentType);

            request.AddOrUpdateProperty(
                RequestIdKey, request.GetCorrelationId());
        }
    }
}
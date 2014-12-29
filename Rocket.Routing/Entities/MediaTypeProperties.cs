// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypeProperties.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypeProperties type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using Rocket.Web.Extensions;

namespace Rocket.Routing.Entities
{
    internal sealed class MediaTypeProperties
    {
        private const string RequestedVersionKey = "VersionConstraint_RequestedVersion";
        private const string ActualVersionKey = "VersionConstraint_ActualVersion";
        private const string ContentTypeKey = "VersionConstraint_ContentType";
        private const string RequestIdKey = "VersionConstraint_RequestId";
        private const string IsMatchedKey = "VersionConstraint_IsMatched";

        private readonly HttpRequestMessage _request;

        public MediaTypeProperties(
            HttpRequestMessage request)
        {
            _request = request;
        }

        public double? RequestedVersion
        {
            get
            {
                return _request.GetProperty<double?>(RequestedVersionKey);
            }

            set
            {
                _request.AddOrUpdateProperty(RequestedVersionKey, value);
            }
        }

        public double ActualVersion
        {
            get
            {
                return _request.GetProperty<double>(ActualVersionKey);
            }

            set
            {
                _request.AddOrUpdateProperty(ActualVersionKey, value);
            }
        }

        public ContentType ContentType
        {
            get
            {
                return _request.GetProperty<ContentType>(ContentTypeKey);
            }

            set
            {
                _request.AddOrUpdateProperty(ContentTypeKey, value);
            }
        }

        public Guid RequestId
        {
            get
            {
                return _request.GetProperty<Guid>(RequestIdKey);
            }

            set
            {
                _request.AddOrUpdateProperty(RequestIdKey, value);
            }
        }

        public bool IsMatched
        {
            get
            {
                return _request.GetProperty<bool>(IsMatchedKey);
            }

            set
            {
                _request.AddOrUpdateProperty(IsMatchedKey, value);
            }
        }
    }
}
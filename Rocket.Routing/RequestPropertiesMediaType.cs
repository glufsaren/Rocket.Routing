// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestPropertiesMediaType.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestPropertiesMediaType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using Rocket.Web.Extensions;

namespace Rocket.Routing
{
    internal sealed class RequestPropertiesMediaType : MediaType
    {
        private const string RequestedVersionKey = "VersionConstraint_RequestedVersion";
        private const string ActualVersionKey = "VersionConstraint_ActualVersion";
        private const string ContentTypeKey = "VersionConstraint_ContentType";
        private const string RequestIdKey = "VersionConstraint_RequestId";
        private const string IsMatchedKey = "VersionConstraint_IsMatched";

        private readonly HttpRequestMessage _httpRequestMessage;

        public RequestPropertiesMediaType(
                        HttpRequestMessage httpRequestMessage)
        {
            _httpRequestMessage = httpRequestMessage;
        }

        public override double? RequestedVersion
        {
            get
            {
                return _httpRequestMessage
                    .GetProperty<double?>(RequestedVersionKey);
            }

            set
            {
                _httpRequestMessage
                    .AddOrUpdateProperty(RequestedVersionKey, value);
            }
        }

        public override double ActualVersion
        {
            get
            {
                return _httpRequestMessage
                    .GetProperty<double>(ActualVersionKey);
            }

            set
            {
                _httpRequestMessage
                    .AddOrUpdateProperty(ActualVersionKey, value);
            }
        }

        public override ContentType ContentType
        {
            get
            {
                return _httpRequestMessage
                    .GetProperty<ContentType>(ContentTypeKey);
            }

            set
            {
                _httpRequestMessage
                    .AddOrUpdateProperty(ContentTypeKey, value);
            }
        }

        public override Guid RequestId
        {
            get
            {
                return _httpRequestMessage
                    .GetProperty<Guid>(RequestIdKey);
            }

            set
            {
                _httpRequestMessage
                    .AddOrUpdateProperty(RequestIdKey, value);
            }
        }

        public override bool Matched
        {
            get
            {
                return _httpRequestMessage
                    .GetProperty<bool>(IsMatchedKey);
            }

            set
            {
                _httpRequestMessage
                    .AddOrUpdateProperty(IsMatchedKey, value);
            }
        }
    }
}
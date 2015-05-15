// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionConstraint.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VersionConstraint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Routing;

using Rocket.Core.Diagnostics;
using Rocket.Routing.Services.Contracts;
using Rocket.Web.Extensions;

namespace Rocket.Routing
{
    internal sealed class VersionConstraint : IHttpRouteConstraint
    {
        private const string AcceptHeader = "accept";

        private readonly bool _isLatest;
        private readonly double _version;

        public VersionConstraint(
            double version, bool isLatest)
        {
            _isLatest = isLatest;
            _version = version;
        }

        public IRoutingService RoutingService { private get; set; }

        public ILog Log { private get; set; }

        public bool Match(
            HttpRequestMessage httpRequestMessage,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            if (routeDirection != HttpRouteDirection.UriResolution)
            {
                return false;
            }

            BuildUp(httpRequestMessage);

            return Match(
                httpRequestMessage.Headers);
        }

        internal bool Match(HttpRequestHeaders httpRequestHeaders)
        {
            var acceptHeaderValue =
                httpRequestHeaders.TryGetHeader(AcceptHeader);

            Log.DebugFormat(
                    "Route request for: Header; [{0}]; Version: [{1}]; Latest: [{2}]",
                    acceptHeaderValue,
                    _version,
                    _isLatest);

            return RoutingService.Match(
                acceptHeaderValue, _version, _isLatest);
        }

        private void BuildUp(HttpRequestMessage httpRequestMessage)
        {
            HttpConfiguration httpConfiguration;
            if (httpRequestMessage.Properties.ContainsKey("MS_HttpConfiguration"))
            {
                httpConfiguration = httpRequestMessage.Properties["MS_HttpConfiguration"] as HttpConfiguration;
            }
            else
            {
                httpConfiguration = GlobalConfiguration.Configuration;
            }

            Bootstrapper.Initialize(httpConfiguration);

            RoutingService = httpRequestMessage.GetService<IRoutingService>();

            Log = httpRequestMessage.GetService<ILog>();
        }
    }
}
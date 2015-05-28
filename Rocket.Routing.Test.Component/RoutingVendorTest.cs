﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingVendorTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingVendorTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using NUnit.Framework;

using Rocket.Routing.Test.Component.TestDoubles;
using Rocket.Test;
using Rocket.Web.Extensions;

using Should;

namespace Rocket.Routing.Test.Component
{
    [TestFixture]
    public class RoutingVendorTest
    {
        [TestFixture]
        public class When_wrong_vendor_is_specified_in_accept_header : BaseComponentTest
        {
            private Result<string> _result;
            private HttpServerHost _httpServerHostHost;
            private string _requestIdHeader;
            private Dictionary<string, string> _headers;

            protected override void Arrange()
            {
                _headers = new Dictionary<string, string>
                                  {
                                      {
                                          "accept",
                                          "application/vnd.acme.se+json; version=1"
                                      }
                                  };

                _httpServerHostHost = new HttpServerHostBuilder()
                    .AddDependencyResolver(httpServerHost => new AutofacConfig(httpServerHost))
                    .Endpoint("http://localhost:1000/api/orders/")
                    .MapRoute<OrderController>("api/orders");

                Bootstrapper.Initialize(
                    _httpServerHostHost.HttpConfiguration);
            }

            protected override void Act()
            {
                _result = _httpServerHostHost.Execute<string>(
                    HttpMethod.Get, headers: _headers);
            }

            protected override void Assemble()
            {
                _requestIdHeader = _result.HttpResponseMessage
                    .TryGetHeader("X-Rocket-Request-Id");
            }

            protected override void TearDown()
            {
                _httpServerHostHost.Dispose();
                Bootstrapper.Reset();
            }

            [Test]
            public void It_should_not_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }

            [Test]
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }
    }
}
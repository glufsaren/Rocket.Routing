// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingVersionTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingVersionTest type.
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
    public class RoutingVersionTest
    {
        [TestFixture]
        public class When_a_non_existing_version_is_specified_in_accept_header : BaseComponentTest
        {
            private Result<string> _result;
            private ApiHost _apiHost;
            private string _requestIdHeader;
            private Dictionary<string, string> _headers;

            protected override void Arrange()
            {
                _headers = new Dictionary<string, string>
                                  {
                                      {
                                          "accept",
                                          "application/vnd.rocket.se+json; version=3"
                                      }
                                  };

                _apiHost = new ApiHostBuilder()
                    .Resolver(new AutofacConfig())
                    .On<OrderController>("http://localhost:1000/api/orders/")
                    .Endpoint();

                Bootstrapper.Initialize(
                    _apiHost.HttpConfiguration);
            }

            protected override void Act()
            {
                _result = _apiHost.Get<string>(headers: _headers);
            }

            protected override void Assemble()
            {
                _requestIdHeader = _result.HttpResponseMessage
                    .TryGetHeader("X-Rocket-Request-Id");
            }

            protected override void TearDown()
            {
                _apiHost.Dispose();
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

        [TestFixture]
        public class When_an_invalid_version_is_specified_in_accept_header : BaseComponentTest
        {
            private Result<string> _result;
            private ApiHost _apiHost;
            private string _requestIdHeader;
            private Dictionary<string, string> _headers;

            protected override void Arrange()
            {
                _headers = new Dictionary<string, string>
                                  {
                                      {
                                          "accept",
                                          "application/vnd.rocket.se+json; version=X"
                                      }
                                  };

                _apiHost = new ApiHostBuilder()
                    .Resolver(new AutofacConfig())
                    .On<OrderController>("http://localhost:1000/api/orders/")
                    .Endpoint();

                Bootstrapper.Initialize(
                    _apiHost.HttpConfiguration);
            }

            protected override void Act()
            {
                _result = _apiHost.Get<string>(headers: _headers);
            }

            protected override void Assemble()
            {
                _requestIdHeader = _result.HttpResponseMessage
                    .TryGetHeader("X-Rocket-Request-Id");
            }

            protected override void TearDown()
            {
                _apiHost.Dispose();
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

        [TestFixture]
        public class When_no_version_is_specified_in_accept_header : BaseComponentTest
        {
            private Result<string> _result;
            private ApiHost _apiHost;
            private string _mediaTypeHeader;
            private string _requestIdHeader;
            private Dictionary<string, string> _headers;

            protected override void Arrange()
            {
                _headers = new Dictionary<string, string>
                                  {
                                      {
                                          "accept",
                                          "application/vnd.rocket.se+json"
                                      }
                                  };

                _apiHost = new ApiHostBuilder()
                    .Resolver(new AutofacConfig())
                    .On<OrderController>("http://localhost:1000/api/orders/")
                    .Endpoint();

                Bootstrapper.Initialize(
                    _apiHost.HttpConfiguration);
            }

            protected override void Act()
            {
                _result = _apiHost.Get<string>(headers: _headers);
            }

            protected override void Assemble()
            {
                _mediaTypeHeader = _result.HttpResponseMessage
                    .TryGetHeader("X-Rocket-Media-Type");

                _requestIdHeader = _result.HttpResponseMessage
                    .TryGetHeader("X-Rocket-Request-Id");
            }

            protected override void TearDown()
            {
                _apiHost.Dispose();
                Bootstrapper.Reset();
            }

            [Test]
            public void It_should_match_latest()
            {
                _result.ResultObject.ShouldEqual("{\"version\":\"2\",\"isLatest\":\"true\"}");
            }

            [Test]
            public void It_should_add_actual_version_in_response()
            {
                _mediaTypeHeader.ShouldEqual("Rocket.v2; format=json;");
            }

            [Test]
            public void It_should_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.OK);
            }

            [Test]
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }
    }
}
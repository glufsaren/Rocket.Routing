﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;

using NUnit.Framework;

using Rocket.Routing.Test.Component.TestDoubles;
using Rocket.Test;
using Rocket.Web.Extensions;

using Should;

namespace Rocket.Routing.Test.Component
{
    [TestFixture]
    public class RoutingTest
    {
        [TestFixture]
        public class When_no_accept_header_is_specified_and_latest_is_specified : BaseComponentTest
        {
            private Result<string> _result;
            private ApiHost _apiHost;
            private string _mediaTypeHeader;
            private string _requestIdHeader;

            protected override void Arrange()
            {
                _apiHost = new ApiHostBuilder()
                    .Resolver(new AutofacConfig())
                    .On<OrderController>("http://localhost:1000/api/orders/")
                    .Endpoint();

                Bootstrapper.Initialize(
                    _apiHost.HttpConfiguration);
            }

            protected override void Act()
            {
                _result = _apiHost.Get<string>();
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
            public void It_should_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.OK);
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
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }

        [TestFixture]
        public class When_a_specific_accept_header_is_specified : BaseComponentTest
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
                                          "application/vnd.rocket.se+json; version=1"
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
            public void It_should_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.OK);
            }

            [Test]
            public void It_should_match_the_specified_version()
            {
                _result.ResultObject.ShouldEqual("{\"version\":\"1\",\"isLatest\":\"false\"}");
            }

            [Test]
            public void It_should_add_actual_version_in_response()
            {
                _mediaTypeHeader.ShouldEqual("Rocket.v1; format=json;");
            }

            [Test]
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }

        [TestFixture]
        public class When_an_ordinary_accept_header_is_specified : BaseComponentTest
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
                                          "application/json"
                                      }
                                  };

                _apiHost =
                    new ApiHostBuilder()
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
            public void It_should_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.OK);
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
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }

        [TestFixture]
        public class When_an_empty_accept_header_is_specified : BaseComponentTest
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
                                          string.Empty
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
            public void It_should_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.OK);
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
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }

        [TestFixture]
        public class When_no_content_type_or_version_are_specified : BaseComponentTest
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
                                          "application/vnd.rocket.se"
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
            public void It_should_find_a_route()
            {
                _result.HttpResponseMessage
                    .StatusCode.ShouldEqual(HttpStatusCode.OK);
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
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldNotEqual(Guid.Empty.ToString());
            }
        }

        [TestFixture]
        public class When_an_invalid_accept_header_is_specified : BaseComponentTest
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
                                          "helt/fel.se"
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
    }
}
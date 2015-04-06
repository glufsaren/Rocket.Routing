// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestPropertiesAcceptHeaderStoreTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestPropertiesAcceptHeaderStoreTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using Moq;

using NUnit.Framework;

using Rocket.Routing.Model;
using Rocket.Routing.Model.Entities;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services;
using Rocket.Routing.Services.Contracts;
using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class RequestPropertiesAcceptHeaderStoreTest
    {
        [TestFixture]
        public class When_setting_accept_header_that_does_not_match : BaseUnitTest
        {
            private RequestPropertiesAcceptHeaderStoreService _requestPropertiesAcceptHeaderStoreService;
            private MediaType _mediaType;

            protected override void Arrange()
            {
                var httpRequestMessageResolver =
                    new Mock<IHttpRequestMessageResolverService>();

                httpRequestMessageResolver
                        .Setup(m => m.Current())
                        .Returns(new HttpRequestMessage());

                var requestIdProvider =
                    new Mock<IRequestIdService>();

                requestIdProvider
                    .Setup(m => m.Get())
                    .Returns(new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));

                _requestPropertiesAcceptHeaderStoreService = new RequestPropertiesAcceptHeaderStoreService(
                    requestIdProvider.Object, httpRequestMessageResolver.Object);

                var acceptHeader = CreateAcceptHeader();
                acceptHeader.MatchHeaderVersion(1, false);

                _requestPropertiesAcceptHeaderStoreService
                    .Set(acceptHeader);
            }

            private static AcceptHeader CreateAcceptHeader()
            {
                return new AcceptHeader(ContentType.Xml, 2.1);
            }

            protected override void Act()
            {
                _mediaType = _requestPropertiesAcceptHeaderStoreService.Get();
            }

            [Test]
            public void It_does_not_set_requested_version()
            {
                _mediaType.RequestedVersion.ShouldBeNull();
            }

            [Test]
            public void It_does_not_set_content_type()
            {
                _mediaType.ContentType.ShouldEqual(ContentType.Unspecified);
            }

            [Test]
            public void It_does_not_set_actual_verison()
            {
                _mediaType.ActualVersion.ShouldEqual(0);
            }

            [Test]
            public void It_should_not_match()
            {
                _mediaType.Matched.ShouldBeFalse();
            }

            [Test]
            public void It_should_set_request_id()
            {
                _mediaType.RequestId.ShouldEqual(
                    new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));
            }
        }

        [TestFixture]
        public class When_setting_accept_header_that_matches : BaseUnitTest
        {
            private RequestPropertiesAcceptHeaderStoreService _requestPropertiesAcceptHeaderStoreService;
            private MediaType _mediaType;

            protected override void Arrange()
            {
                var httpRequestMessageResolver =
                    new Mock<IHttpRequestMessageResolverService>();

                httpRequestMessageResolver
                        .Setup(m => m.Current())
                        .Returns(new HttpRequestMessage());

                var requestIdProvider =
                    new Mock<IRequestIdService>();

                requestIdProvider
                    .Setup(m => m.Get())
                    .Returns(new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));

                _requestPropertiesAcceptHeaderStoreService = new RequestPropertiesAcceptHeaderStoreService(
                    requestIdProvider.Object, httpRequestMessageResolver.Object);

                var acceptHeader = CreateAcceptHeader();
                acceptHeader.MatchHeaderVersion(2.1, true);

                _requestPropertiesAcceptHeaderStoreService
                    .Set(acceptHeader);
            }

            private static AcceptHeader CreateAcceptHeader()
            {
                return new AcceptHeader(ContentType.Xml, 2.1);
            }

            protected override void Act()
            {
                _mediaType = _requestPropertiesAcceptHeaderStoreService.Get();
            }

            [Test]
            public void It_should_set_requested_version()
            {
                _mediaType.RequestedVersion.ShouldEqual(2.1);
            }

            [Test]
            public void It_should_set_content_type()
            {
                _mediaType.ContentType.ShouldEqual(ContentType.Xml);
            }

            [Test]
            public void It_should_set_actual_verison()
            {
                _mediaType.ActualVersion.ShouldEqual(2.1);
            }

            [Test]
            public void It_should_not_match()
            {
                _mediaType.Matched.ShouldBeTrue();
            }

            [Test]
            public void It_should_set_request_id()
            {
                _mediaType.RequestId.ShouldEqual(
                    new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));
            }
        }
    }
}
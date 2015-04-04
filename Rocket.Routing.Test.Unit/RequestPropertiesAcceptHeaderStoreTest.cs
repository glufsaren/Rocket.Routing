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
            private RequestPropertiesAcceptHeaderStore _requestPropertiesAcceptHeaderStore;
            private MediaType _mediaType;

            protected override void Arrange()
            {
                var httpRequestMessageResolver =
                    new Mock<IHttpRequestMessageResolver>();

                httpRequestMessageResolver
                        .Setup(m => m.Current())
                        .Returns(new HttpRequestMessage());

                var requestIdProvider =
                    new Mock<IRequestIdProvider>();

                requestIdProvider
                    .Setup(m => m.Get())
                    .Returns(new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));

                _requestPropertiesAcceptHeaderStore = new RequestPropertiesAcceptHeaderStore(
                    requestIdProvider.Object, httpRequestMessageResolver.Object);

                var acceptHeader = CreateAcceptHeader();
                acceptHeader.MatchHeaderVersion(1, false);

                _requestPropertiesAcceptHeaderStore
                    .Set(acceptHeader);
            }

            private static AcceptHeader CreateAcceptHeader()
            {
                return new AcceptHeader
                           {
                               ContentType = ContentType.Xml,
                               RequestedVersion = 2.1
                           };
            }

            protected override void Act()
            {
                _mediaType = _requestPropertiesAcceptHeaderStore.Get();
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
            private RequestPropertiesAcceptHeaderStore _requestPropertiesAcceptHeaderStore;
            private MediaType _mediaType;

            protected override void Arrange()
            {
                var httpRequestMessageResolver =
                    new Mock<IHttpRequestMessageResolver>();

                httpRequestMessageResolver
                        .Setup(m => m.Current())
                        .Returns(new HttpRequestMessage());

                var requestIdProvider =
                    new Mock<IRequestIdProvider>();

                requestIdProvider
                    .Setup(m => m.Get())
                    .Returns(new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));

                _requestPropertiesAcceptHeaderStore = new RequestPropertiesAcceptHeaderStore(
                    requestIdProvider.Object, httpRequestMessageResolver.Object);

                var acceptHeader = CreateAcceptHeader();
                acceptHeader.MatchHeaderVersion(2.1, true);

                _requestPropertiesAcceptHeaderStore
                    .Set(acceptHeader);
            }

            private static AcceptHeader CreateAcceptHeader()
            {
                return new AcceptHeader
                           {
                               ContentType = ContentType.Xml,
                               RequestedVersion = 2.1
                           };
            }

            protected override void Act()
            {
                _mediaType = _requestPropertiesAcceptHeaderStore.Get();
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
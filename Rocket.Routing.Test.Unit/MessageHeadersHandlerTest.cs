// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageHeadersHandlerTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MessageHeadersHandlerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using Moq;

using NUnit.Framework;

using Rocket.Routing.Model;
using Rocket.Routing.Model.Entities;
using Rocket.Routing.Services.Contracts;
using Rocket.Test;

using Rocket.Web.Extensions;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class MessageHeadersHandlerTest
    {
        public class When_adding_response_headers : BaseUnitTest
        {
            private MessageHeadersHandler _messageHeadersHandler;
            private HttpResponseMessage _responseMessage;
            private string _mediaTypeHeader;
            private string _requestIdHeader;

            protected override void Arrange()
            {
                var mediaType = new MediaType
                                    {
                                        ActualVersion = 1.2,
                                        ContentType = ContentType.Xml,
                                        Matched = true,
                                        RequestedVersion = 1,
                                        RequestId = new Guid("55B3D509-D610-403E-8D61-5C66AD64F1D5")
                                    };

                var vendorNameProvider = new Mock<IVendorNameService>();
                vendorNameProvider
                    .Setup(m => m.GetName())
                    .Returns("rocket");

                var acceptHeaderStore = new Mock<IAcceptHeaderStoreService>();
                acceptHeaderStore.Setup(m => m.Get()).Returns(mediaType);

                var requestIdProvider = new Mock<IRequestIdService>(MockBehavior.Strict);

                _messageHeadersHandler = new MessageHeadersHandler
                                             {
                                                 VendorNameService = vendorNameProvider.Object,
                                                 AcceptHeaderStoreService = acceptHeaderStore.Object,
                                                 RequestIdService = requestIdProvider.Object
                                             };

                _responseMessage = new HttpResponseMessage();
            }

            protected override void Act()
            {
                _messageHeadersHandler.AddResponseHeaders(_responseMessage);
            }

            protected override void Assemble()
            {
                _mediaTypeHeader = _responseMessage
                    .TryGetHeader("X-Rocket-Media-Type");

                _requestIdHeader = _responseMessage
                    .TryGetHeader("X-Rocket-Request-Id");
            }

            [Test]
            public void It_should_add_actual_version_in_response()
            {
                _mediaTypeHeader.ShouldEqual("Rocket.v1.2; format=xml;");
            }

            [Test]
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldEqual(
                    new Guid("55B3D509-D610-403E-8D61-5C66AD64F1D5").ToString());
            }
        }

        public class When_adding_response_headers_and_media_type_has_no_request_id : BaseUnitTest
        {
            private MessageHeadersHandler _messageHeadersHandler;
            private HttpResponseMessage _responseMessage;
            private string _mediaTypeHeader;
            private string _requestIdHeader;

            protected override void Arrange()
            {
                var mediaType = new MediaType
                {
                    ActualVersion = 1.2,
                    ContentType = ContentType.Xml,
                    Matched = true,
                    RequestedVersion = 1,
                    RequestId = Guid.Empty
                };

                var vendorNameProvider = new Mock<IVendorNameService>();
                vendorNameProvider
                    .Setup(m => m.GetName())
                    .Returns("rocket");

                var acceptHeaderStore = new Mock<IAcceptHeaderStoreService>();
                acceptHeaderStore.Setup(m => m.Get()).Returns(mediaType);

                var requestIdProvider = new Mock<IRequestIdService>();
                requestIdProvider
                    .Setup(m => m.Get())
                    .Returns(new Guid("DC81D99E-F73E-4DBB-8273-75C92A1D3C83"));

                _messageHeadersHandler = new MessageHeadersHandler
                {
                    VendorNameService = vendorNameProvider.Object,
                    AcceptHeaderStoreService = acceptHeaderStore.Object,
                    RequestIdService = requestIdProvider.Object
                };

                _responseMessage = new HttpResponseMessage();
            }

            protected override void Act()
            {
                _messageHeadersHandler.AddResponseHeaders(_responseMessage);
            }

            protected override void Assemble()
            {
                _mediaTypeHeader = _responseMessage
                    .TryGetHeader("X-Rocket-Media-Type");

                _requestIdHeader = _responseMessage
                    .TryGetHeader("X-Rocket-Request-Id");
            }

            [Test]
            public void It_should_add_actual_version_in_response()
            {
                _mediaTypeHeader.ShouldEqual("Rocket.v1.2; format=xml;");
            }

            [Test]
            public void It_should_add_request_id_in_response()
            {
                _requestIdHeader.ShouldEqual(
                    new Guid("DC81D99E-F73E-4DBB-8273-75C92A1D3C83").ToString());
            }
        }
    }
}
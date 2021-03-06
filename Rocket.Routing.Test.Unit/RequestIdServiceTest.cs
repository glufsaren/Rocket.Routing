﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestIdServiceTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RequestIdServiceTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Web.Http.Hosting;

using Moq;

using NUnit.Framework;

using Rocket.Routing.Services;
using Rocket.Routing.Services.Contracts;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class RequestIdServiceTest
    {
        [Test]
        public void When_no_request_is_resolved_expect_empty_request_id()
        {
            var httpRequestMessageResolver =
                new Mock<IHttpRequestMessageResolverService>();

            httpRequestMessageResolver
                    .Setup(m => m.Current())
                    .Returns((HttpRequestMessage)null);

            var requestIdProvider = new RequestIdService(
                httpRequestMessageResolver.Object);

            var requestId = requestIdProvider.Get();

            requestId.ShouldEqual(Guid.Empty);
        }

        [Test]
        public void When_request_is_resolved_expect_correlation_id_as_request_id()
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Properties.Add(
                HttpPropertyKeys.RequestCorrelationKey,
                new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));

            var httpRequestMessageResolver =
                new Mock<IHttpRequestMessageResolverService>();

            httpRequestMessageResolver
                    .Setup(m => m.Current())
                    .Returns(httpRequestMessage);

            var requestIdProvider = new RequestIdService(
                httpRequestMessageResolver.Object);

            var requestId = requestIdProvider.Get();

            requestId.ShouldEqual(
                new Guid("757DB7D0-859E-4E9E-ABA0-23D312F18541"));
        }
    }
}
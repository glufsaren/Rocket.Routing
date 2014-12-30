// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypePropertiesTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypePropertiesTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

using NUnit.Framework;

using Rocket.Routing.Entities;

using Should;

namespace Rocket.Routing.Test.Unit.Entities
{
    [TestFixture]
    public class MediaTypePropertiesTest
    {
        [Test]
        public void When_getting_not_specified_requested_version_expect_default_value()
        {
            var httpRequestMessage = new HttpRequestMessage();
            var mediaTypeProperties =
                new MediaTypeProperties(httpRequestMessage);

            mediaTypeProperties.RequestedVersion.ShouldBeNull();
        }

        [Test]
        public void When_getting_requested_version_expect_value()
        {
            var httpRequestMessage = new HttpRequestMessage();
            var mediaTypeProperties = 
                new MediaTypeProperties(httpRequestMessage);

            mediaTypeProperties.RequestedVersion = 3;

            mediaTypeProperties.RequestedVersion.ShouldEqual(3);
        }

        [Test]
        public void When_getting_actual_version_expect_value()
        {
            var httpRequestMessage = new HttpRequestMessage();
            var mediaTypeProperties =
                new MediaTypeProperties(httpRequestMessage);

            mediaTypeProperties.ActualVersion = 1;

            mediaTypeProperties.ActualVersion.ShouldEqual(1);
        }

        [Test]
        public void When_getting_content_type_expect_value()
        {
            var httpRequestMessage = new HttpRequestMessage();
            var mediaTypeProperties =
                new MediaTypeProperties(httpRequestMessage);

            mediaTypeProperties.ContentType = ContentType.Xml;

            mediaTypeProperties.ContentType.ShouldEqual(ContentType.Xml);
        }

        [Test]
        public void When_getting_request_id_expect_value()
        {
            var httpRequestMessage = new HttpRequestMessage();
            var mediaTypeProperties =
                new MediaTypeProperties(httpRequestMessage);

            mediaTypeProperties.RequestId = 
                new Guid("757DFD7C-7872-4C02-A390-B5C2468AAD02");

            mediaTypeProperties.RequestId.ShouldEqual(
                new Guid("757DFD7C-7872-4C02-A390-B5C2468AAD02"));
        }

        [Test]
        public void When_getting_matched_expect_value()
        {
            var httpRequestMessage = new HttpRequestMessage();
            var mediaTypeProperties =
                new MediaTypeProperties(httpRequestMessage);

            mediaTypeProperties.Matched = true;

            mediaTypeProperties.Matched.ShouldBeTrue();
        }
    }
}
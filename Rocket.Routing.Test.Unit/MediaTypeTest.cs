// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypeTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypeTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

using Rocket.Routing.Model;
using Rocket.Routing.Model.Entities;
using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class MediaTypeTest
    {
        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_getting_media_type_header_name_with_no_vendor_name_expect_name_returned(string vendorName)
        {
            var mediaTypeHeaderName =
                MediaType.GetMediaTypeHeaderName(vendorName);

            mediaTypeHeaderName.ShouldEqual("X-Media-Type");
        }

        [Test]
        public void When_getting_media_type_header_name_expect_name_returned()
        {
            var mediaTypeHeaderName =
                MediaType.GetMediaTypeHeaderName("Rocket");

            mediaTypeHeaderName.ShouldEqual("X-Rocket-Media-Type");
        }

        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_getting_request_id_header_name_with_no_vendor_name_expect_name_returned(string vendorName)
        {
            var mediaTypeHeaderName =
                MediaType.GetRequestIdHeaderName(vendorName);

            mediaTypeHeaderName.ShouldEqual("X-Request-Id");
        }

        [Test]
        public void When_getting_request_id_header_name_expect_name_returned()
        {
            var mediaTypeHeaderName =
                MediaType.GetRequestIdHeaderName("Rocket");

            mediaTypeHeaderName.ShouldEqual("X-Rocket-Request-Id");
        }

        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_getting_media_type_header_with_no_vendor_name_expect_header_returned(string vendorName)
        {
            var mediaType = new MediaType
                                {
                                    ActualVersion = 2.1,
                                    ContentType = ContentType.Xml
                                };

            var mediaTypeHeader = mediaType
                .GetMediaTypeString(vendorName);

            mediaTypeHeader.ShouldEqual("v2.1; format=xml;");
        }

        [Test]
        public void When_getting_media_type_header_with_no_content_type_expect_header_returned()
        {
            var mediaType = new MediaType
                                {
                                    ActualVersion = 2.1,
                                    ContentType = ContentType.Unspecified
                                };

            var mediaTypeHeader = mediaType
                .GetMediaTypeString("Rocket");

            mediaTypeHeader.ShouldEqual("Rocket.v2.1;");
        }

        [Test]
        public void When_getting_media_type_header_expect_header_returned()
        {
            var mediaType = new MediaType
                                {
                                    ActualVersion = 2.1,
                                    ContentType = ContentType.Xml
                                };

            var mediaTypeHeader = mediaType
                .GetMediaTypeString("Rocket");

            mediaTypeHeader.ShouldEqual("Rocket.v2.1; format=xml;");
        }
    }
}
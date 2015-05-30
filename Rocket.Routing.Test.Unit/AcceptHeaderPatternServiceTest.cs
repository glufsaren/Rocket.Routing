// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderPatternServiceTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderPatternServiceTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Moq;

using NUnit.Framework;

using Rocket.Routing.Services;
using Rocket.Routing.Services.Contracts;
using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class AcceptHeaderPatternServiceTest
    {
        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_vendor_name_is_null_or_empty_expect_no_exception(string vendorName)
        {
            var vendorNameProvider = new Mock<IVendorNameService>();

            vendorNameProvider
                .Setup(m => m.GetName())
                .Returns(vendorName);

            vendorNameProvider
                .Setup(m => m.GetPlaceHolder())
                .Returns("#NAME");

            vendorNameProvider
                .Setup(m => m.GetPattern())
                .Returns("#NAME");

            var acceptHeaderPatternProvider =
                new AcceptHeaderPatternService(vendorNameProvider.Object);

            var acceptHeaderPattern = acceptHeaderPatternProvider.Get();

            acceptHeaderPattern.ShouldEqual("acme");
        }

        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_pattern_is_null_or_empty_expect_default_pattern_used(string pattern)
        {
            var vendorNameProvider = new Mock<IVendorNameService>();

            vendorNameProvider
                .Setup(m => m.GetName())
                .Returns("rocket");

            vendorNameProvider
                .Setup(m => m.GetPlaceHolder())
                .Returns("#VENDOR_NAME#");

            vendorNameProvider
                .Setup(m => m.GetPattern())
                .Returns(pattern);

            var acceptHeaderPatternProvider =
                new AcceptHeaderPatternService(vendorNameProvider.Object);

            var acceptHeaderPattern = acceptHeaderPatternProvider.Get();

            var expected = DefaultVendorNameService
                .CustomMediaTypePattern.Replace("#VENDOR_NAME#", "rocket");

            acceptHeaderPattern.ShouldEqual(expected);
        }

        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_place_holder_is_null_or_empty_expect_no_exception(string placeHolder)
        {
            var vendorNameProvider = new Mock<IVendorNameService>();

            vendorNameProvider
                .Setup(m => m.GetName())
                .Returns("rocket");

            vendorNameProvider
                .Setup(m => m.GetPlaceHolder())
                .Returns(placeHolder);

            vendorNameProvider
                .Setup(m => m.GetPattern())
                .Returns("PATTERN");

            var acceptHeaderPatternProvider =
                new AcceptHeaderPatternService(vendorNameProvider.Object);

            var acceptHeaderPattern = acceptHeaderPatternProvider.Get();

            acceptHeaderPattern.ShouldEqual("PATTERN");
        }

        [Test]
        public void When_pattern_is_not_found_expect_no_exception()
        {
            var vendorNameProvider = new Mock<IVendorNameService>();

            vendorNameProvider
                .Setup(m => m.GetName())
                .Returns("rocket");

            vendorNameProvider
                .Setup(m => m.GetPlaceHolder())
                .Returns("#NAME");

            vendorNameProvider
                .Setup(m => m.GetPattern())
                .Returns("NO_PLACE_HOLDER");

            var acceptHeaderPatternProvider =
                new AcceptHeaderPatternService(vendorNameProvider.Object);

            var acceptHeaderPattern = acceptHeaderPatternProvider.Get();

            acceptHeaderPattern.ShouldEqual("NO_PLACE_HOLDER");
        }

        [Test]
        public void When_specifying_name_pattern_and_place_holder_expect_replaced_pattern()
        {
            var vendorNameProvider = new Mock<IVendorNameService>();

            vendorNameProvider
                .Setup(m => m.GetName())
                .Returns("rocket");

            vendorNameProvider
                .Setup(m => m.GetPlaceHolder())
                .Returns("#NAME");

            vendorNameProvider
                .Setup(m => m.GetPattern())
                .Returns("PATTERN_#NAME_PATTERN");

            var acceptHeaderPatternProvider =
                new AcceptHeaderPatternService(vendorNameProvider.Object);

            var acceptHeaderPattern = acceptHeaderPatternProvider.Get();

            acceptHeaderPattern.ShouldEqual("PATTERN_rocket_PATTERN");
        }
    }
}
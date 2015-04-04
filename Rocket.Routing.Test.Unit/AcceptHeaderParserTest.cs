// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderParserTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderParserTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Moq;

using NUnit.Framework;

using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class AcceptHeaderParserTest
    {
        private const string Pattern =
            @"^(\+?(?<contenttype>[a-zA-Z0-9-\.]*?));?(\sversion=(?<version>\d+(\.\d+)*);?)?$";

        [Test]
        public void When_pattern_is_null_expect_null()
        {
            var acceptHeaderPatternProvider =
                new Mock<IAcceptHeaderPatternProvider>();

            acceptHeaderPatternProvider
                .Setup(m => m.Get())
                .Returns((string)null);

            var acceptHeaderParser = new AcceptHeaderParser(
                acceptHeaderPatternProvider.Object);

            var acceptHeader = acceptHeaderParser
                .Parse("application/vnd.rocket.se+json; version=1;");

            acceptHeader.ShouldBeNull();
        }

        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_header_is_null_or_empty_expect_null(string headerValue)
        {
            var acceptHeaderPatternProvider =
                new Mock<IAcceptHeaderPatternProvider>();

            acceptHeaderPatternProvider
                 .Setup(m => m.Get())
                 .Returns(Pattern);

            var acceptHeaderParser = new AcceptHeaderParser(
                acceptHeaderPatternProvider.Object);

            var acceptHeader =
                acceptHeaderParser.Parse(headerValue);

            acceptHeader.ShouldBeNull();
        }
    }
}
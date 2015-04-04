// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutingServiceTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the RoutingServiceTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Moq;

using NUnit.Framework;

using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class RoutingServiceTest
    {
        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        public void When_header_is_null_or_empty_expect_default_accept_header(string acceptHeaderValue)
        {
            AcceptHeader acceptHeader = null;

            var acceptHeaderStore =
                new Mock<IAcceptHeaderStore>();

            acceptHeaderStore
                .Setup(m => m.Set(It.IsAny<AcceptHeader>()))
                .Callback<AcceptHeader>(arg => { acceptHeader = arg; });

            var headerParser =
                new Mock<IHeaderParser<AcceptHeader>>(MockBehavior.Strict);

            var routingService = new RoutingService(
                acceptHeaderStore.Object, headerParser.Object);

            var matches = routingService.Match(acceptHeaderValue, 1, false);

            matches.ShouldBeFalse();
            acceptHeader.RequestedVersion.ShouldBeNull();
            acceptHeader.ContentType.ShouldEqual(ContentType.Json);
        }

        [Test]
        public void When_parsed_header_is_null_expect_no_match()
        {
            var acceptHeaderStore =
                new Mock<IAcceptHeaderStore>(MockBehavior.Strict);

            var headerParser =
                new Mock<IHeaderParser<AcceptHeader>>();
            headerParser
                .Setup(m => m.Parse(It.IsAny<string>()))
                .Returns((AcceptHeader)null);

            var routingService = new RoutingService(
                acceptHeaderStore.Object, headerParser.Object);

            var matches = routingService.Match("Header", 1, false);

            matches.ShouldBeFalse();
        }

        [TestFixture]
        public class When_accept_header_matches : BaseUnitTest
        {
            private AcceptHeader _storedAcceptHeader;
            private AcceptHeader _acceptHeader;
            private bool _matches;
            private RoutingService _routingService;

            protected override void Arrange()
            {
                var acceptHeaderStore = new Mock<IAcceptHeaderStore>();

                acceptHeaderStore
                    .Setup(m => m.Set(It.IsAny<AcceptHeader>()))
                    .Callback<AcceptHeader>(arg => { _storedAcceptHeader = arg; });

                var headerParser = new Mock<IHeaderParser<AcceptHeader>>();

                _acceptHeader = CreateAcceptHeader();
                _acceptHeader.MatchHeaderVersion(1, true);

                headerParser
                    .Setup(m => m.Parse(It.IsAny<string>()))
                    .Returns(_acceptHeader);

                _routingService = new RoutingService(
                    acceptHeaderStore.Object, headerParser.Object);
            }

            protected override void Act()
            {
                _matches = _routingService.Match("Header", 1, true);
            }

            [Test]
            public void It_should_match()
            {
                _matches.ShouldBeFalse();
            }

            [Test]
            public void It_stores_the_matched_header()
            {
                _storedAcceptHeader.ShouldBeSameAs(_acceptHeader);
            }

            private static AcceptHeader CreateAcceptHeader()
            {
                return new AcceptHeader
                           {
                               ContentType = ContentType.Xml,
                               RequestedVersion = 2.1
                           };
            }
        }
    }
}
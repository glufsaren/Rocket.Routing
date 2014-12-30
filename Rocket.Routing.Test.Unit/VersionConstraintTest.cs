// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionConstraintTEst.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VersionConstraintTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;

using Moq;

using NUnit.Framework;

using Rocket.Routing.Contracts;
using Rocket.Routing.Entities;
using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class VersionConstraintTest
    {
        [Test]
        public void When_parser_returns_null_while_matching_expect_false()
        {
            var versionConstraint = new VersionConstraint(2, false);
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add(
                "Accept", "application/vnd.rocket+json; version=1");

            var headerParser =
                new Mock<IHeaderParser<AcceptHeader>>();
            headerParser
                .Setup(m => m.Parse("application/vnd.rocket+json; version=1"))
                .Returns((AcceptHeader)null);

            var acceptHeaderStore =
                new Mock<IAcceptHeaderStore>(MockBehavior.Strict);

            var matches = versionConstraint.Match(
                httpRequestMessage,
                headerParser.Object,
                acceptHeaderStore.Object);

            matches.ShouldBeFalse();
        }

        [TestFixture]
        public class When_matching_accept_header : BaseUnitTest
        {
            private VersionConstraint _versionConstraint;

            private HttpRequestMessage _httpRequestMessage;
            private Mock<IHeaderParser<AcceptHeader>> _headerParser;
            private Mock<IAcceptHeaderStore> _acceptHeaderStore;

            private AcceptHeader _acceptHeader;

            private bool _matches;

            protected override void Arrange()
            {
                _versionConstraint = new VersionConstraint(1, false);
                _acceptHeader = CreateAcceptHeader();

                _httpRequestMessage = new HttpRequestMessage();
                _httpRequestMessage.Headers.Add(
                    "Accept", "application/vnd.rocket+json; version=1");

                _headerParser =
                    new Mock<IHeaderParser<AcceptHeader>>();
                _headerParser
                    .Setup(m => m.Parse("application/vnd.rocket+json; version=1"))
                    .Returns(_acceptHeader);

                _acceptHeaderStore =
                    new Mock<IAcceptHeaderStore>();
            }

            protected override void Act()
            {
                _matches = _versionConstraint.Match(
                                        _httpRequestMessage,
                                        _headerParser.Object,
                                        _acceptHeaderStore.Object);
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _matches.ShouldBeTrue();
            }

            [Test]
            public void It_stores_the_accept_header_information()
            {
                _acceptHeaderStore
                    .Verify(m => m.Set(_acceptHeader));
            }

            private static AcceptHeader CreateAcceptHeader(
                double requestedVersion = 1)
            {
                return new AcceptHeader
                           {
                               ContentType = ContentType.Json,
                               RequestedVersion = requestedVersion
                           };
            }
        }
    }
}
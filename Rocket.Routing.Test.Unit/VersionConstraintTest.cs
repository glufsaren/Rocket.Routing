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

using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class VersionConstraintTest
    {
        [TestFixture]
        public class When_matching_accept_header : BaseUnitTest
        {
            private VersionConstraint _versionConstraint;
            private HttpRequestMessage _httpRequestMessage;
            private Mock<IRoutingService> _routingService;

            private AcceptHeader _acceptHeader;
            private bool _matches;

            protected override void Arrange()
            {
                _acceptHeader = CreateAcceptHeader();
                _acceptHeader.MatchHeaderVersion(1, true);

                _httpRequestMessage = new HttpRequestMessage();
                _httpRequestMessage.Headers.Add(
                    "Accept", "application/vnd.rocket+json; version=1");

                _routingService =
                    new Mock<IRoutingService>();
                _routingService
                    .Setup(m => m.Match("application/vnd.rocket+json; version=1", 1, true))
                    .Returns(true);

                _versionConstraint = new VersionConstraint(1, true)
                                         {
                                             RoutingService = _routingService.Object
                                         };
            }

            protected override void Act()
            {
                _matches = _versionConstraint
                    .Match(_httpRequestMessage.Headers);
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _matches.ShouldBeTrue();
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
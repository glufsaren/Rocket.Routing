// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

using Rocket.Routing.Entities;
using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit.Entities
{
    [TestFixture]
    public class AcceptHeaderTest
    {
        [TestFixture]
        public class When_creating_accept_header : BaseUnitTest
        {
            private AcceptHeader _acceptHeader;

            protected override void Arrange()
            {
            }

            protected override void Act()
            {
                _acceptHeader = new AcceptHeader();
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _acceptHeader.RequestedVersion.ShouldEqual(0);
            }

            [Test]
            public void It_initializes_content_type()
            {
                _acceptHeader.ContentType.ShouldEqual(ContentType.Json);
            }
        }

        [TestFixture]
        public class When_accept_header_matches_latest_version : BaseUnitTest
        {
            private AcceptHeader _acceptHeader;

            protected override void Arrange()
            {
                _acceptHeader = new AcceptHeader
                                    {
                                        RequestedVersion = null
                                    };
            }

            protected override void Act()
            {
                _acceptHeader.MatchHeaderVersion(2, true);
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _acceptHeader.ActualVersion.ShouldEqual(2);
            }

            [Test]
            public void It_initializes_content_type()
            {
                _acceptHeader.Matches.ShouldBeTrue();
            }
        }

        [TestFixture]
        public class When_accept_header_does_not_match_latest_version : BaseUnitTest
        {
            private AcceptHeader _acceptHeader;

            protected override void Arrange()
            {
                _acceptHeader = new AcceptHeader
                {
                    RequestedVersion = null
                };
            }

            protected override void Act()
            {
                _acceptHeader.MatchHeaderVersion(1, false);
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _acceptHeader.ActualVersion.ShouldEqual(0);
            }

            [Test]
            public void It_initializes_content_type()
            {
                _acceptHeader.Matches.ShouldBeFalse();
            }
        }

        [TestFixture]
        public class When_accept_header_matches_specific_version : BaseUnitTest
        {
            private AcceptHeader _acceptHeader;

            protected override void Arrange()
            {
                _acceptHeader = new AcceptHeader
                {
                    RequestedVersion = 1
                };
            }

            protected override void Act()
            {
                _acceptHeader.MatchHeaderVersion(1, false);
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _acceptHeader.ActualVersion.ShouldEqual(1);
            }

            [Test]
            public void It_initializes_content_type()
            {
                _acceptHeader.Matches.ShouldBeTrue();
            }
        }

        [TestFixture]
        public class When_accept_header_does_not_match_requested_version : BaseUnitTest
        {
            private AcceptHeader _acceptHeader;

            protected override void Arrange()
            {
                _acceptHeader = new AcceptHeader
                {
                    RequestedVersion = 1
                };
            }

            protected override void Act()
            {
                _acceptHeader.MatchHeaderVersion(2, false);
            }

            [Test]
            public void It_initializes_requested_verison()
            {
                _acceptHeader.ActualVersion.ShouldEqual(0);
            }

            [Test]
            public void It_initializes_content_type()
            {
                _acceptHeader.Matches.ShouldBeFalse();
            }
        }
    }
}
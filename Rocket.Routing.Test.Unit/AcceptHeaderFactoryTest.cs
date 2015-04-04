// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderFactoryTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderFactoryTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

using NUnit.Framework;

using Rocket.Test;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class AcceptHeaderFactoryTest
    {
        [TestFixture]
        public class When_creating_default : BaseUnitTest
        {
            private AcceptHeader _acceptHeader;

            protected override void Arrange()
            {
            }

            protected override void Act()
            {
                _acceptHeader = AcceptHeaderFactory.Default();
            }

            [Test]
            public void It_sets_requested_version_to_null()
            {
                _acceptHeader.RequestedVersion.ShouldBeNull();
            }

            [Test]
            public void It_sets_content_type_to_json()
            {
                _acceptHeader.ContentType.ShouldEqual(ContentType.Json);
            }

            [TestFixture]
            public class When_creating_from_empty_match : BaseUnitTest
            {
                private AcceptHeader _acceptHeader;

                private Match _match;

                protected override void Arrange()
                {
                    _match = Match.Empty;
                }

                protected override void Act()
                {
                    _acceptHeader = AcceptHeaderFactory.Create(_match);
                }

                [Test]
                public void It_sets_requested_version_to_null()
                {
                    _acceptHeader.RequestedVersion.ShouldBeNull();
                }

                [Test]
                public void It_sets_content_type_to_json()
                {
                    _acceptHeader.ContentType.ShouldEqual(ContentType.Json);
                }
            }

            [TestFixture]
            public class When_creating_from_match : BaseUnitTest
            {
                private AcceptHeader _acceptHeader;

                private Match _match;

                protected override void Arrange()
                {
                    _match = Regex.Match(
                            "+xml; version=1.5;", 
                            @"^(\+?(?<contenttype>[a-zA-Z0-9-\.]*?));?(\sversion=(?<version>\d+(\.\d+)*);?)?$");
                }

                protected override void Act()
                {
                    _acceptHeader = AcceptHeaderFactory.Create(_match);
                }

                [Test]
                public void It_sets_requested_version_to_null()
                {
                    _acceptHeader.RequestedVersion.ShouldEqual(1.5);
                }

                [Test]
                public void It_sets_content_type_to_json()
                {
                    _acceptHeader.ContentType.ShouldEqual(ContentType.Xml);
                }
            }
        }
    }
}
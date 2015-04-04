// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaTypeExtensionsTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaTypeExtensionsTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class MediaTypeExtensionsTest
    {
        [Test]
        public void When_media_type_is_null_expect_false()
        {
            MediaType mediaType = null;

            mediaType.HasRequestId().ShouldBeFalse();
        }

        [Test]
        public void When_media_type_is_empty_expect_false()
        {
            var mediaType = new MediaType();

            mediaType.HasRequestId().ShouldBeFalse();
        }
    }
}
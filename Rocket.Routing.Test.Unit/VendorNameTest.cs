// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VendorNameTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VendorNameTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NUnit.Framework;

using Rocket.Routing.Model.ValueObjects;

using Should;

namespace Rocket.Routing.Test.Unit
{
    [TestFixture]
    public class VendorNameTest
    {
        [Test]
        public void When_vendor_name_is_null_expect_empty_value()
        {
            var vendorName = new VendorName(null);

            vendorName.Value.ShouldEqual(string.Empty);
        }

        [Test]
        public void When_creating_vendor_name_expect_formatted_name_returned()
        {
            var vendorName = new VendorName("ROCKET");

            vendorName.Value.ShouldEqual("Rocket");
        }
    }
}
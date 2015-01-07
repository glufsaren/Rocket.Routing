// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AcceptHeaderParserTest.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the AcceptHeaderParserTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Moq;

using NUnit.Framework;

using Rocket.Routing.Http;
using Rocket.Test;

namespace Rocket.Routing.Test.Unit.Http
{
    [TestFixture]
    public class AcceptHeaderParserTest
    {
        [TestCaseSource(typeof(TestCaseSources), "NullOrWhitespace")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_header_value_is_null_or_empty(string headerValue)
        {
            var settingsReader = 
                new Mock<ISettingsReader>(MockBehavior.Strict);

            var vendorNameProvider = 
                new Mock<IVendorNameProvider>(MockBehavior.Strict);

            var acceptHeaderParser = new AcceptHeaderParser(
                settingsReader.Object, vendorNameProvider.Object);

            acceptHeaderParser.Parse(headerValue);
        }
    }
}
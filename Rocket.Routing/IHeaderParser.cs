﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHeaderParser.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IHeaderParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing
{
    internal interface IHeaderParser
    {
        MediaType Parse(string acceptHeader);
    }
}
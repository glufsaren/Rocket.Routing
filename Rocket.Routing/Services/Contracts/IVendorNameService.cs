// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVendorNameService.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the IVendorNameService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Rocket.Routing.Services.Contracts
{
    public interface IVendorNameService
    {
        string GetName();

        string GetPlaceHolder();

        string GetPattern();
    }
}
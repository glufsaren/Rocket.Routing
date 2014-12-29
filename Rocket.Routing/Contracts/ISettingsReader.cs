// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsReader.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines types that implement functionality for reading application settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Rocket.Routing.Contracts
{
    /// <summary>
    /// Defines types that implement functionality for reading application settings.
    /// </summary>
    public interface ISettingsReader
    {
        /// <summary>
        /// Gets the application setting for the specified <see cref="key"/>.
        /// </summary>
        /// <typeparam name="T">The type of the settings value.</typeparam>
        /// <param name="key">The key for the setting.</param>
        /// <returns>The application settings value.</returns>
        T GetAppSetting<T>(string key) where T : IConvertible;
    }
}
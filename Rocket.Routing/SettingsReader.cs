// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsReader.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Class used for reading application settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;

namespace Rocket.Routing
{
    /// <summary>
    /// Class used for reading application settings.
    /// </summary>
    public class SettingsReader : ISettingsReader
    {
        /// <summary>
        /// Gets the application setting for the specified <see cref="key"/>.
        /// </summary>
        /// <typeparam name="T">The type of the settings value.</typeparam>
        /// <param name="key">The key for the setting.</param>
        /// <returns>The application settings value.</returns>
        public T GetAppSetting<T>(string key) where T : IConvertible
        {
            return NullOrDefault<T>(
                ConfigurationManager.AppSettings[key]);
        }

        private static T NullOrDefault<T>(object value, T defaultValue = default(T))
            where T : IConvertible
        {
            if (value == null || Convert.IsDBNull(value))
            {
                return defaultValue;
            }

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
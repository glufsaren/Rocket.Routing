// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtension.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the HttpRequestMessageExtension type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;

namespace Rocket.Routing.Extensions
{
    internal static class HttpRequestMessageExtension
    {
        public static string TryGetHeader(
            this HttpRequestMessage httpRequestMessage, string headerName)
        {
            if (httpRequestMessage == null)
            {
                throw new ArgumentNullException("httpRequestMessage");
            }

            return httpRequestMessage
                .Headers.TryGetHeader(headerName);
        }

        public static T GetService<T>(this HttpRequestMessage httpRequestMessage)
        {
            return (T)httpRequestMessage
                            .GetDependencyScope()
                            .GetService(typeof(T));
        }

        public static void AddOrUpdateProperty(
            this HttpRequestMessage request, string key, object value)
        {
            if (request.Properties.ContainsKey(key))
            {
                request.Properties[key] = value;
                return;
            }

            request.Properties.Add(key, value);
        }

        public static T GetProperty<T>(
            this HttpRequestMessage request, string key)
        {
            return !request.Properties.ContainsKey(key)
                ? default(T)
                : (T)request.Properties[key];
        }
    }
}
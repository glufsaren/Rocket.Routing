// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestHeadersExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the HttpRequestHeadersExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Rocket.Routing.Extensions
{
    internal static class HttpRequestHeadersExtensions
    {
        public static string TryGetHeader(
            this HttpRequestHeaders httpRequestHeaders, string headerName)
        {
            if (httpRequestHeaders == null)
            {
                throw new ArgumentNullException("httpRequestHeaders");
            }

            IEnumerable<string> headerValues;

            if (!httpRequestHeaders
                .TryGetValues(headerName, out headerValues))
            {
                return null;
            }

            var headerValuesList =
                headerValues.Enumerate();

            return headerValuesList.Count() == 1
                    ? headerValuesList.First()
                    : null;
        } 
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestMessageResolver.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the HttpRequestMessageResolver type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Web;

using JetBrains.Annotations;

namespace Rocket.Routing
{
    [UsedImplicitly]
    public class HttpRequestMessageResolver : IHttpRequestMessageResolver
    {
        public HttpRequestMessage Current()
        {
            return HttpContext.Current
                .Items["MS_HttpRequestMessage"] as HttpRequestMessage;
        }
    }
}
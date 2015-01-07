// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerBuilderExtensions.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the ContainerBuilderExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Web;

using Autofac;

namespace Rocket.Routing.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static HttpRequestMessage CurrentRequest(this ContainerBuilder containerBuilder)
        {
            return HttpContext.Current
                .Items["MS_HttpRequestMessage"] as HttpRequestMessage;
        }
    }
}
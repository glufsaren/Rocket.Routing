// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageHeadersHandler.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MessageHeadersHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Rocket.Web.Extensions;

namespace Rocket.Routing
{
    public class MessageHeadersHandler : DelegatingHandler
    {
        internal static void AddMediaTypeDataInResponse(
            HttpRequestMessage requestMessage,
            HttpResponseMessage responseMessage)
        {
            var vendorNameProvider = requestMessage
                .GetService<IVendorNameProvider>();

            var acceptHeaderStore = requestMessage
                .GetService<IAcceptHeaderStore>();

            var requestIdProvider = requestMessage
                .GetService<IRequestIdProvider>();

            var mediaType = acceptHeaderStore.Get();

            var vendorName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase((vendorNameProvider.GetName() ?? string.Empty).ToLower());
            var mediaTypeString = GetMediaTypeString(mediaType, vendorName);

            var mediaTypeHeaderName = string.Format("X-{0}-Media-Type", vendorName);
            var requestIdHeaderName = string.Format("X-{0}-Request-Id", vendorName);

            responseMessage.Headers.Add(mediaTypeHeaderName, mediaTypeString);
            responseMessage.Headers.Add(requestIdHeaderName, GetRequestId(mediaType, requestIdProvider));
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage =
                await base.SendAsync(requestMessage, cancellationToken);

            AddMediaTypeDataInResponse(requestMessage, responseMessage);

            return responseMessage;
        }

        private static bool HasRequestId(MediaType mediaType)
        {
            return mediaType != null && mediaType.RequestId != Guid.Empty;
        }

        private static string GetRequestId(MediaType mediaType, IRequestIdProvider requestIdProvider)
        {
            return HasRequestId(mediaType)
                       ? mediaType.RequestId.ToString()
                       : requestIdProvider.Get().ToString();
        }

        private static string GetMediaTypeString(MediaType mediaType, string vendorName)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("{0}.v{1};", vendorName, mediaType.ActualVersion);

            if (mediaType.ContentType != ContentType.Unspecified)
            {
                stringBuilder.AppendFormat(" format={0};", mediaType.ContentType.ToString().ToLower());
            }

            return stringBuilder.ToString();
        }
    }
}
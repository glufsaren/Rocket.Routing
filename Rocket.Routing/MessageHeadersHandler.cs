// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageHeadersHandler.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MessageHeadersHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rocket.Routing
{
    public class MessageHeadersHandler : DelegatingHandler
    {
        internal static void AddMediaTypeDataInResponse(
            HttpRequestMessage requestMessage,
            HttpResponseMessage responseMessage)
        {
            var mediaType = new MediaTypeProperties(requestMessage);

            var mediaTypeString = GetMediaTypeString(mediaType);

            responseMessage.Headers.Add("X-Acme-Media-Type", mediaTypeString);
            responseMessage.Headers.Add("X-Acme-Request-Id", mediaType.RequestId.ToString());
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage =
                await base.SendAsync(requestMessage, cancellationToken);

            AddMediaTypeDataInResponse(requestMessage, responseMessage);

            return responseMessage;
        }

        private static string GetMediaTypeString(MediaTypeProperties mediaType)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("acme.v{0};", mediaType.ActualVersion);

            if (mediaType.ContentType != ContentType.Unspecified)
            {
                stringBuilder.AppendFormat(" format={0};", mediaType.ContentType.ToString().ToLower());
            }

            return stringBuilder.ToString();
        }
    }
}
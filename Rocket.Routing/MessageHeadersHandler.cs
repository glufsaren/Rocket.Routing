// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageHeadersHandler.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MessageHeadersHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
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
            var mediaType = MediaType.FromRequest(requestMessage);

            var res = string.Format(
                "acme.v{0}; format={1}",
                mediaType.ActualVersion,
                mediaType.ContentType.ToString().ToLower());

            responseMessage.Headers.Add("X-Acme-Media-Type", res);
            responseMessage.Headers.Add("X-Acme-Request-Id", mediaType.RequestId.ToString());
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            //var messageMetaData =
            //    requestMessage.GetService<MediaType>();

            //messageMetaData.RequestId = requestMessage.GetCorrelationId();

            HttpResponseMessage responseMessage =
                await base.SendAsync(requestMessage, cancellationToken);

            AddMediaTypeDataInResponse(requestMessage, responseMessage);

            return responseMessage;
        }
    }
}
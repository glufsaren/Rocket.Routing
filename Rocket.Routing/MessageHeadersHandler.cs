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

using Rocket.Routing.Model.ValueObjects;
using Rocket.Web.Extensions;

namespace Rocket.Routing
{
    public class MessageHeadersHandler : DelegatingHandler
    {
        public IVendorNameProvider VendorNameProvider { get; set; }

        public IAcceptHeaderStore AcceptHeaderStore { get; set; }

        public IRequestIdProvider RequestIdProvider { get; set; }

        internal void AddResponseHeaders(
            HttpResponseMessage responseMessage)
        {
            var vendorName =
                new VendorName(VendorNameProvider.GetName());

            var mediaType = AcceptHeaderStore.Get();

            responseMessage.Headers.Add(
                MediaType.GetMediaTypeHeaderName(vendorName.Value),
                mediaType.GetMediaTypeString(vendorName.Value));

            responseMessage.Headers.Add(
                MediaType.GetRequestIdHeaderName(vendorName.Value),
                GetRequestId(mediaType, RequestIdProvider));
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            BuildUp(requestMessage);

            HttpResponseMessage responseMessage =
                await base.SendAsync(requestMessage, cancellationToken);

            AddResponseHeaders(responseMessage);

            return responseMessage;
        }

        private static string GetRequestId(
            MediaType mediaType, IRequestIdProvider requestIdProvider)
        {
            return mediaType.HasRequestId()
                    ? mediaType.RequestId.ToString()
                    : requestIdProvider.Get().ToString();
        }

        private void BuildUp(HttpRequestMessage requestMessage)
        {
            VendorNameProvider = requestMessage
                .GetService<IVendorNameProvider>();

            AcceptHeaderStore = requestMessage
                .GetService<IAcceptHeaderStore>();

            RequestIdProvider = requestMessage
                .GetService<IRequestIdProvider>();
        }
    }
}
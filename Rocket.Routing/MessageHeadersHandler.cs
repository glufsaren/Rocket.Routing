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
using System.Web.Http;

using Rocket.Routing.Model;
using Rocket.Routing.Model.Entities;
using Rocket.Routing.Model.ValueObjects;
using Rocket.Routing.Services.Contracts;
using Rocket.Web.Extensions;

namespace Rocket.Routing
{
    public class MessageHeadersHandler : DelegatingHandler
    {
        public IVendorNameService VendorNameService { get; set; }

        public IAcceptHeaderStoreService AcceptHeaderStoreService { get; set; }

        public IRequestIdService RequestIdService { get; set; }

        internal void AddResponseHeaders(
            HttpResponseMessage responseMessage)
        {
            var vendorName =
                new VendorName(VendorNameService.GetName());

            var mediaType = AcceptHeaderStoreService.Get();

            responseMessage.Headers.Add(
                MediaType.GetMediaTypeHeaderName(vendorName.Value),
                mediaType.GetMediaTypeString(vendorName.Value));

            responseMessage.Headers.Add(
                MediaType.GetRequestIdHeaderName(vendorName.Value),
                GetRequestId(mediaType, RequestIdService));
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
            MediaType mediaType, IRequestIdService requestIdService)
        {
            return mediaType.HasRequestId()
                    ? mediaType.RequestId.ToString()
                    : requestIdService.Get().ToString();
        }

        private void BuildUp(HttpRequestMessage httpRequestMessage)
        {
            HttpConfiguration httpConfiguration;
            if (httpRequestMessage.Properties.ContainsKey("MS_HttpConfiguration"))
            {
                httpConfiguration = httpRequestMessage.Properties["MS_HttpConfiguration"] as HttpConfiguration;
            }
            else
            {
                httpConfiguration = GlobalConfiguration.Configuration;
            }

            Bootstrapper.Initialize(httpConfiguration);

            VendorNameService = httpRequestMessage
                .GetService<IVendorNameService>();

            AcceptHeaderStoreService = httpRequestMessage
                .GetService<IAcceptHeaderStoreService>();

            RequestIdService = httpRequestMessage
                .GetService<IRequestIdService>();
        }
    }
}
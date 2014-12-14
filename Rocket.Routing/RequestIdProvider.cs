using System;
using System.Net.Http;

namespace Rocket.Routing
{
    public class RequestIdProvider : IRequestIdProvider
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public RequestIdProvider(HttpRequestMessage httpRequestMessage)
        {
            _httpRequestMessage = httpRequestMessage;
        }

        public Guid Get()
        {
            return _httpRequestMessage.GetCorrelationId();
        }
    }
}
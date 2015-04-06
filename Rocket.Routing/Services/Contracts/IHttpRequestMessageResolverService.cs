using System.Net.Http;

namespace Rocket.Routing.Services.Contracts
{
    public interface IHttpRequestMessageResolverService
    {
        HttpRequestMessage Current();
    }
}
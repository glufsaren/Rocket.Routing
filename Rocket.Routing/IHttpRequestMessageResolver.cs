using System.Net.Http;

namespace Rocket.Routing
{
    public interface IHttpRequestMessageResolver
    {
        HttpRequestMessage Current();
    }
}
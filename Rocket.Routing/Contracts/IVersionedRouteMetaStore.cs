namespace Rocket.Routing.Contracts
{
    public interface IVersionedRouteMetaStore
    {
        void Set();

        void Get();
    }

    public sealed class VersionedRouteMetaStore : IVersionedRouteMetaStore
    {
        public void Set()
        {
        }

        public void Get()
        {
        }
    }
}
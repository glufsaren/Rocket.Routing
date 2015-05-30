using System;

using Rocket.Routing.Services.Contracts;

namespace Routing
{
    public class DefaultRequestIdService : IRequestIdService
    {
        public Guid Get()
        {
            return Guid.NewGuid();
        }
    }
}
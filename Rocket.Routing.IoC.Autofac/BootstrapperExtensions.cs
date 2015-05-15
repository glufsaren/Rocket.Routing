using Autofac;

namespace Rocket.Routing.IoC.Autofac
{
    public static class BootstrapperExtensions
    {
        public static IBootstrapper On(
            this IBootstrapper bootstrapper, 
            ContainerBuilder containerBuilder)
        {
            //((RoutingModule)bootstrapper).ContainerBuilder = containerBuilder;

            return bootstrapper;
        }
    }
}
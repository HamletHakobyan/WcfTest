using Autofac;
using WcfTest.Clinet;
using WcfTest.Clinet.Callbacks;
using WcfTest.Contracts.Service;

namespace WcfConsumer
{
    public static class AutofacBootstrapper
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventHandler>()
                .AsSelf()
                .As<IEventHandler>()
                .SingleInstance();
            builder.RegisterType<EventBroker>()
                .As<IEventBroker>()
                .SingleInstance();
            builder.RegisterType<EventHandlerRegistrarClient>()
                .As<IEventHandlerRegistrar>()
                .SingleInstance();

            return builder.Build();
        }
    }
}
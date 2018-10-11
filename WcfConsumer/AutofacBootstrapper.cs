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
            builder.RegisterType<EventBroker>()
                .As<IEventSubscriber>()
                .SingleInstance();
            //builder.RegisterType<EventHandlerRegistrarClient>()
            //    .As<IEventHandlerRegistrar>()
            //    .SingleInstance();

            return builder.Build();
        }
    }
}
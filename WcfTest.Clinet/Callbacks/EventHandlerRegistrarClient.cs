using System.ServiceModel;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet.Callbacks
{
    public class EventHandlerRegistrarClient : DuplexClientBase<IEventHandlerRegistrar>, IEventHandlerRegistrar
    {
        public EventHandlerRegistrarClient(IEventHandler eventHandler)
            : base(new InstanceContext(eventHandler))
        {
        }

        public void Register()
        {
            Channel.Register();
        }
    }
}
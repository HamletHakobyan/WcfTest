using System.ServiceModel;
using WcfTest.Clinet.Callbacks;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class EventHandlerRegistrarClient : DuplexClientBase<IEventHandlerRegistrar>, IEventHandlerRegistrar
    {
        public EventHandlerRegistrarClient(IEventHandler eventHandler)
            : base(new InstanceContext(eventHandler))
        {
            Channel.Register();
        }

        public void Register()
        {
        }
    }
}
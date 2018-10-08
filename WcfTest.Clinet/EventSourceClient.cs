using System.ServiceModel;
using WcfTest.Clinet.Callbacks;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class EventSourceClient : DuplexClientBase<IEventRegistrar>, IEventRegistrar
    {
        public EventSourceClient(IEventHandler eventHandler) : base(new InstanceContext(eventHandler))
        {
            Channel.Register();
        }

        public void Register()
        {
        }

        public IEventHandler CallbackChannel { get; }
    }
}
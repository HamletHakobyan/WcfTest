using System.ServiceModel;
using WcfTest.Contracts;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class EventSubscriberClient : DuplexClientBase<IEventSubscriber>, IEventSubscriber
    {
        public EventSubscriberClient(IEventPublisher eventPublisher)
            : base(new InstanceContext(eventPublisher))
        {
            
        }
        public void Subscribe<T>() where T : IEvent
        {
            throw new System.NotImplementedException();
        }

        public void CallSubscribers<T>(T @event) where T : IEvent
        {
            throw new System.NotImplementedException();
        }
    }
}
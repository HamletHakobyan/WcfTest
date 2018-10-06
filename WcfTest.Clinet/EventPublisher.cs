using WcfTest.Contracts;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventSubscriber _eventSubscriber;

        public EventPublisher(IEventSubscriber eventSubscriber)
        {
            _eventSubscriber = eventSubscriber;
        }
        public void Publish<T>(T @event) where T : IEvent
        {
            _eventSubscriber.CallSubscribers(@event);
        }
    }
}
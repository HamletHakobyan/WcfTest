using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventHandlerSource _eventHandlerSource;

        public EventPublisher(IEventHandlerSource eventHandlerSource)
        {
            _eventHandlerSource = eventHandlerSource;
        }
        public void Publish<T>(T @event) where T : EventDataBase
        {
            _eventHandlerSource.CallbackChannel?.Publish(typeof(T).FullName, @event);
        }
    }
}
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class EventHandler : IEventHandler
    {
        private readonly IEventHandlerSource _eventHandlerSource;

        public EventHandler(IEventHandlerSource eventHandlerSource)
        {
            _eventHandlerSource = eventHandlerSource;
        }

        public void PublishDoubleReturned(DoubleReturned doubleReturned)
        {
            _eventHandlerSource.CallbackChannel?.PublishDoubleReturned(doubleReturned);
        }

        public void PublishTrippleReturned(TrippleReturned trippleReturned)
        {
            _eventHandlerSource.CallbackChannel?.PublishTrippleReturned(trippleReturned);
        }

        public string PublishNeedData(NeedData needData)
        {
            return _eventHandlerSource.CallbackChannel?.PublishNeedData(needData);

        }
    }
}
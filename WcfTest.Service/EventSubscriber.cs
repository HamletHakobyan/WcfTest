using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfTest.Contracts;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class EventSubscriber : IEventSubscriber
    {
        private readonly Dictionary<Type, List<IEventPublisher>> _publishers =
            new Dictionary<Type, List<IEventPublisher>>();

        public void Subscribe<T>() where T : IEvent
        {
            var callbackChannel = OperationContext.Current.GetCallbackChannel<IEventPublisher>();
            var type = typeof(T);

            if (!_publishers.ContainsKey(type))
            {
                _publishers.Add(type, new List<IEventPublisher>());
            }

            _publishers[type].Add(callbackChannel);
        }

        public void CallSubscribers<T>(T @event) where T : IEvent
        {
            foreach (var eventPublisher in _publishers[typeof(T)])
            {
                eventPublisher.Publish(@event);
            }
        }
    }
}
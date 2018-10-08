using System;
using System.Collections.Generic;
using System.Linq;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet.Callbacks
{
    public interface IEventBroker
    {
        void Publish<T>(T @event) where T : EventDataBase;
        void Subscribe<T>(Action<T> action) where T : EventDataBase;
    }

    public class EventBroker : IEventBroker
    {
        private readonly List<Delegate> _subscribers = new List<Delegate>();
        public void Publish<T>(T @event) where T : EventDataBase
        {
            foreach (var action in _subscribers.OfType<Action<T>>())
            {
                action(@event);
            }
        }

        public void Subscribe<T>(Action<T> action) where T : EventDataBase
        {
            _subscribers.Add(action);
        }
    }
}
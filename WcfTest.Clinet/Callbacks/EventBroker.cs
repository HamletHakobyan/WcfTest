using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet.Callbacks
{
    public class EventBroker : IEventHandler, IEventSubscriber
    {
        private readonly Dictionary<Type, List<Delegate>> _actionsStore = new Dictionary<Type, List<Delegate>>();
        private readonly Dictionary<Type, Delegate> _functionStore = new Dictionary<Type, Delegate>();
        public EventBroker()
        {
            new EventHandlerRegistrarClient(this).Register();
        }

        public void PublishDoubleReturned(DoubleReturned doubleReturned)
        {
            var actions = _actionsStore[typeof(DoubleReturned)];
            if (actions == null)
            {
                return;
            }

            foreach (var action in actions.OfType<Action<DoubleReturned>>())
            {
                action(doubleReturned);
            }
        }

        public void PublishTrippleReturned(TrippleReturned trippleReturned)
        {
            var actions = _actionsStore[typeof(TrippleReturned)];
            if (actions == null)
            {
                return;
            }

            foreach (var action in actions.OfType<Action<TrippleReturned>>())
            {
                action(trippleReturned);
            }
        }

        public string PublishNeedData(NeedData needData)
        {
            return ((Func<NeedData, string>)_functionStore[typeof(NeedData)])(needData);
        }

        public void Subscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            if (!_actionsStore.ContainsKey(type))
            {
                _actionsStore.Add(type, new List<Delegate>());
            }

            _actionsStore[type].Add(action);
        }

        public void Subscribe<T, U>(Func<T, U> func)
        {
            _functionStore[typeof(T)] = func;
        }
    }
}
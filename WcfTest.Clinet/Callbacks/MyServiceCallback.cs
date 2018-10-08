﻿using System;
using System.Linq;
using System.Reflection;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet.Callbacks
{
    public class MyServiceCallback : IMyServiceCallback
    {
        private readonly IEventBroker _eventBroker;

        public MyServiceCallback(IEventBroker eventBroker)
        {
            _eventBroker = eventBroker;
        }
        public void Publish(string typeFullName, EventDataBase trippleReturned)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == typeFullName);
            if (type == null)
            {
                return;

            }
            var method = typeof(IEventBroker).GetMethod("Publish");
            var generic = method?.MakeGenericMethod(type);
            generic?.Invoke(_eventBroker, new object[] { trippleReturned });
        }
    }
}
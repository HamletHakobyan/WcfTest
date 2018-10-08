using System;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet.Callbacks
{
    public interface IEventBroker
    {
        void Publish<T>(T @event) where T : EventDataBase;
        void Subscribe<T>(Action<T> action) where T : EventDataBase;
    }
}
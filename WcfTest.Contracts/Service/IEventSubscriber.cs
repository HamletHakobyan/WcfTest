using System;

namespace WcfTest.Contracts.Service
{
    public interface IEventSubscriber
    {
        void Subscribe<T>(Action<T> action);
        void Subscribe<T, U>(Func<T, U> func);
    }
}
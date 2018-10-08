using WcfTest.Contracts.Data;

namespace WcfTest.Contracts.Service
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : EventDataBase;
    }
}
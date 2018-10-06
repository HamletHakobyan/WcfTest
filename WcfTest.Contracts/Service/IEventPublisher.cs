using System.ServiceModel;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IEventPublisher
    {
        [OperationContract]
        void Publish<T>(T @event) where T : IEvent;
    }
}
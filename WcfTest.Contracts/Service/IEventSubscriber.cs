using System.ServiceModel;

namespace WcfTest.Contracts.Service
{
    [ServiceContract(CallbackContract = typeof(IEventPublisher))]
    public interface IEventSubscriber
    {
        [OperationContract]
        void Subscribe<T>() where T : IEvent;
        void CallSubscribers<T>(T @event) where T : IEvent;
    }
}
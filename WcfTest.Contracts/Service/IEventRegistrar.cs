using System.ServiceModel;

namespace WcfTest.Contracts.Service
{
    [ServiceContract(CallbackContract = typeof(IEventHandler))]
    public interface IEventRegistrar
    {
        [OperationContract]
        void Register();

        IEventHandler CallbackChannel { get; }

    }
}
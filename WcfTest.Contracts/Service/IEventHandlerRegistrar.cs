using System.ServiceModel;

namespace WcfTest.Contracts.Service
{
    [ServiceContract(CallbackContract = typeof(IEventHandler))]
    public interface IEventHandlerRegistrar
    {
        [OperationContract]
        void Register();
    }
}
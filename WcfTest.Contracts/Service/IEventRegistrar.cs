using System.ServiceModel;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IEventRegistrar
    {
        [OperationContract]
        void Register();

    }
}
using System.ServiceModel;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IImpersonationProvider
    {
        [OperationContract]
        void SetImpersonationContext();
    }
}
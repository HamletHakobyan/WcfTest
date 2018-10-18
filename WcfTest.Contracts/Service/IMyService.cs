using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        Task<DoubleReturned> GetAgeAsync();
        [OperationContract]
        Task<string> GetName();
        [OperationContract]
        Task<string> GetImpersonatedName(int processId);
        [OperationContract]
        Task<string> GetAttrImpersonationData();

    }
}

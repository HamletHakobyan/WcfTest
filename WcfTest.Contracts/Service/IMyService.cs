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
    }
}

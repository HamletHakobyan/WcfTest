using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        Task<int> GetAgeAsync();
    }
}

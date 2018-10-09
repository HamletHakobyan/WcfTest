using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class MyServiceClinet : ClientBase<IMyService>, IMyService
    {
        public Task<DoubleReturned> GetAgeAsync()
        {
            return Channel.GetAgeAsync();
        }
    }
}

using System.Security.Principal;
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

        public  Task<string> GetImpersonatedName(int processId)
        {
            return Channel.GetImpersonatedName(processId);
        }

        public Task<string> GetAttrImpersonationData()
        {
            return Channel.GetAttrImpersonationData();
        }

        public Task<string> GetName()
        {
            return Channel.GetName();
        }
    }
}

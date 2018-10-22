using System.ServiceModel;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet
{
    public class ImpersonationProviderClient : ClientBase<IImpersonationProvider>, IImpersonationProvider
    {
        public void SetImpersonationContext()
        {
            Channel.SetImpersonationContext();
        }
    }
}
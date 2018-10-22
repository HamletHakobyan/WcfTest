using System;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Win32.SafeHandles;
using WcfTest.Contracts.Service;
using WcfTest.Service.Infrastructure;

namespace WcfTest.Service
{
    public class ImpersonationService : IImpersonationService, IDisposable
    {
        public ImpersonationService()
        {
            
        }
        private SafeAccessTokenHandle _accessToken;

        [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public void SetImpersonationContext()
        {
            _accessToken = WindowsIdentity.GetCurrent(TokenAccessLevels.AllAccess).AccessToken;
        }

        public void RunInContext(Action action)
        {
            WindowsIdentity.RunImpersonated(_accessToken, action);
        }

        public T RunInContext<T>(Func<T> func)
        {
            return WindowsIdentity.RunImpersonated(_accessToken, func);
        }

        public void Dispose()
        {
            _accessToken?.Dispose();
        }
    }
}
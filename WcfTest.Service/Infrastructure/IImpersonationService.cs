using System;
using WcfTest.Contracts.Service;

namespace WcfTest.Service.Infrastructure
{
    public interface IImpersonationService : IImpersonationProvider
    {
        void RunInContext(Action action);
        T RunInContext<T>(Func<T> func);
    }
}
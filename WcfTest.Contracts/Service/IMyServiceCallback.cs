using System;
using System.ServiceModel;
using WcfTest.Contracts.Data;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IMyServiceCallback
    {
        [OperationContract]
        void Publish(string typeFullName, EventDataBase trippleReturned);
    }
}
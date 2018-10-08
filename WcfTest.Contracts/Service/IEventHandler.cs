using System;
using System.ServiceModel;
using WcfTest.Contracts.Data;

namespace WcfTest.Contracts.Service
{
    public interface IEventHandler
    {
        [OperationContract]
        void Publish(string typeFullName, EventDataBase trippleReturned);
    }
}
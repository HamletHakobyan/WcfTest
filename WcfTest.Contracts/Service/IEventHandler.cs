using System;
using System.ServiceModel;
using WcfTest.Contracts.Data;

namespace WcfTest.Contracts.Service
{
    [ServiceContract]
    public interface IEventHandler
    {
        [OperationContract]
        void PublishDoubleReturned(DoubleReturned doubleReturned);
        [OperationContract]
        void PublishTrippleReturned(TrippleReturned trippleReturned);
        [OperationContract]
        string PublishNeedData(NeedData needData);
    }
}
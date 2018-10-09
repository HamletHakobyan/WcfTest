using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class MyService : IMyService
    {
        private readonly IEventHandler _eventHandler;

        public MyService(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }


        public async Task<DoubleReturned> GetAgeAsync()
        {
            var str =_eventHandler.PublishNeedData(new NeedData {InputData = "abc"});
            _eventHandler.PublishDoubleReturned(new DoubleReturned{DoubledValue = str.Length});
            OperationContext.Current.OperationCompleted += Current_OperationCompleted;
            await Task.Delay(2000);
            return new DoubleReturned{DoubledValue = 84};
        }

        private async void Current_OperationCompleted(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            _eventHandler.PublishTrippleReturned(new TrippleReturned { TrippleValue = 3 * 500 });
        }

   }
}

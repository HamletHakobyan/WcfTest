using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class MyService : IMyService
    {
        private IMyServiceCallback _callback;
        public async Task<DoubleReturned> GetAgeAsync()
        {
            _callback = OperationContext.Current.GetCallbackChannel<IMyServiceCallback>();
            OperationContext.Current.OperationCompleted += Current_OperationCompleted;
            await Task.Delay(2000);
            return new DoubleReturned{DoubledValue = 84};
        }

        private async void Current_OperationCompleted(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            _callback.Publish(typeof(TrippleReturned).FullName, new TrippleReturned { TrippleValue = 3 * 42 });
        }
    }
}

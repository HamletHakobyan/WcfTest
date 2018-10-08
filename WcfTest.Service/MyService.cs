using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MyService : IMyService
    {
        private readonly IEventRegistrar _eventSource;

        public MyService(IEventRegistrar eventSource)
        {
            _eventSource = eventSource;
        }

        private IEventHandler Handler => _eventSource.CallbackChannel;

        public async Task<DoubleReturned> GetAgeAsync()
        {
            Handler?.Publish(typeof(TrippleReturned).FullName, new TrippleReturned{TrippleValue = 500});
            OperationContext.Current.OperationCompleted += Current_OperationCompleted;
            await Task.Delay(2000);
            return new DoubleReturned{DoubledValue = 84};
        }

        private async void Current_OperationCompleted(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            Handler?.Publish(typeof(TrippleReturned).FullName, new TrippleReturned { TrippleValue = 3 * 42 });
        }

   }
}

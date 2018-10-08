using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class MyService : IMyService
    {
        private readonly IEventPublisher _eventPublisher;

        public MyService(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }


        public async Task<DoubleReturned> GetAgeAsync()
        {
            _eventPublisher.Publish(new TrippleReturned{TrippleValue = 500});
            OperationContext.Current.OperationCompleted += Current_OperationCompleted;
            await Task.Delay(2000);
            return new DoubleReturned{DoubledValue = 84};
        }

        private async void Current_OperationCompleted(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            _eventPublisher.Publish(new TrippleReturned { TrippleValue = 3 * 42 });
        }

   }
}

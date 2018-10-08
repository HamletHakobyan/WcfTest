using System.ServiceModel;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class EventHandlerSource : IEventHandlerSource
    {
        public void Register()
        {
            CallbackChannel = OperationContext.Current.GetCallbackChannel<IEventHandler>();
        }

        public IEventHandler CallbackChannel { get; private set; }
    }
}
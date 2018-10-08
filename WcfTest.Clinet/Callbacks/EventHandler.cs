using System;
using System.Linq;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfTest.Clinet.Callbacks
{
    public class EventHandler : IEventHandler
    {
        public EventHandler(IEventBroker eventBroker)
        {
            new EventHandlerRegistrarClient(this).Register();
            Broker = eventBroker;
        }
        public void Publish(string typeFullName, EventDataBase trippleReturned)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == typeFullName);
            if (type == null)
            {
                return;

            }
            var method = typeof(IEventBroker).GetMethod("Publish");
            var generic = method?.MakeGenericMethod(type);
            generic?.Invoke(Broker, new object[] { trippleReturned });
        }

        public IEventBroker Broker { get; }
    }
}
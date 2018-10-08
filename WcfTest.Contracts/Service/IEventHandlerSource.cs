namespace WcfTest.Contracts.Service
{
    public interface IEventHandlerSource : IEventHandlerRegistrar
    {
        IEventHandler CallbackChannel { get; }
    }
}
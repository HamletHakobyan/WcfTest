using System.Runtime.Serialization;

namespace WcfTest.Contracts.Data
{
    [DataContract]
    [KnownType(typeof(TrippleReturned))]
    public class EventDataBase
    {
    }
}
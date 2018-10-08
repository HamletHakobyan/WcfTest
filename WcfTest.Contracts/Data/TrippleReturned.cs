using System.Runtime.Serialization;

namespace WcfTest.Contracts.Data
{
    [DataContract]
    public class TrippleReturned : EventDataBase
    {
        [DataMember]
        public int TrippleValue { get; set; }
    }
}
using System.Runtime.Serialization;

namespace WcfTest.Contracts.Data
{
    [DataContract]
    public class TrippleReturned
    { 
        [DataMember]
        public int TrippleValue { get; set; }
    }
}
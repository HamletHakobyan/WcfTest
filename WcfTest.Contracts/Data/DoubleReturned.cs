using System.Runtime.Serialization;

namespace WcfTest.Contracts.Data
{
    [DataContract]
    public class DoubleReturned
    {
        [DataMember]
        public int DoubledValue { get; set; }
    }
}
using System;
using System.Runtime.Serialization;

namespace SSPS.VO
{
    [DataContract]
    public class Supplementation
    {
        [DataMember]
        public DateTime From { get; set; }
        [DataMember]
        public DateTime To { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTime Updated { get; set; }
        
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class CallRequest
    {
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        public List<Field> Fields { get; set; }
    }

    [DataContract]
    public class Field
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Action { get; set; } // It is 'A' or 'E' or 'D'
    }
}
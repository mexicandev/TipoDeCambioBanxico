using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;

namespace TipoDeCambio
{
    [DataContract]
    public class DataSerie
    {
        [DataMember(Name = "fecha")]
        public string Date { get; set; }

        [DataMember(Name = "dato")]
        public string Data { get; set; }
    }
}

using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;

namespace TipoDeCambio
{
    [DataContract]
    public class Serie
    {
        [DataMember(Name = "titulo")]
        public string Title { get; set; }

        [DataMember(Name = "idSerie")]
        public string IdSerie { get; set; }

        [DataMember(Name = "datos")]
        public DataSerie[] Data { get; set; }

    }
}

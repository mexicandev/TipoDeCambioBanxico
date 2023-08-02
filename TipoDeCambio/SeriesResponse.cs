using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;

namespace TipoDeCambio
{
    [DataContract]
    public class SeriesResponse
    {
        [DataMember(Name = "series")]
        public Serie[] series { get; set; }
    }
}

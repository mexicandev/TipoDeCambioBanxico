using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;

namespace TipoDeCambio
{
    [DataContract]
    class Response
    {
        [DataMember(Name = "bmx")]
        public SeriesResponse seriesResponse { get; set; }
    }
}

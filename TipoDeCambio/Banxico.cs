using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TipoDeCambio
{
    public class Banxico
    {
        #region /* Constantes */
        const string BANXICO_MI_TOKEN = "4b31aa07d26b8f5387c33d83abb89c4397c42d30aba1233fd9d39c2027743d99";
        const string BANXICO_URL = "https://www.banxico.org.mx/SieAPIRest/service/v1/series/{0}/datos/{1}/{1}";
        const string BANXICO_FORMATO_FECHA = "yyyy-MM-dd";

        const string BANXICO_HEADER_ITEMTOKEN = "Bmx-Token";
        const string BANXICO_HEADER_FORMATACCEPTED = "application/json";

        const string BANXICO_SERIE_TIPOCAMBIOFIX = "SF43718";
        const string BANXICO_SERIE_TIPOCAMBIOEURO = "SF46410";
        #endregion


        #region /* Procedimientos WebRequest */
        static Response ReadSerie(string serie)
        {
            return ReadSerie(serie, DateTime.Now);
        }

        static Response ReadSerie(string serie, DateTime fecha)
        {

            Response _result = null;
            string _strSerie = serie;
            string _fmtFecha = fecha.ToString(BANXICO_FORMATO_FECHA);

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string _url = string.Format(BANXICO_URL, _strSerie, _fmtFecha);

                HttpWebRequest _webRequest = WebRequest.Create(_url) as HttpWebRequest;
                _webRequest.Accept = BANXICO_HEADER_FORMATACCEPTED;
                _webRequest.Headers[BANXICO_HEADER_ITEMTOKEN] = BANXICO_MI_TOKEN;
                HttpWebResponse _webResponse = _webRequest.GetResponse() as HttpWebResponse;

                if (_webResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    _webResponse.StatusCode,
                    _webResponse.StatusDescription));

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                object objResponse = jsonSerializer.ReadObject(_webResponse.GetResponseStream());
                _result = objResponse as Response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            return _result;
        }
        #endregion


        public static string TipoDeCambioFIX(int tipo)
        {
            string _result = TipoDeCambioFIX(DateTime.Now.AddDays(-1), tipo);
            return _result;
        }

        public static string TipoDeCambioFIX(DateTime fecha, int tipo)
        {
            string _result = string.Empty;
            Response _responce;

            if (tipo == 1 )
                _responce = ReadSerie(BANXICO_SERIE_TIPOCAMBIOFIX, fecha);
            else
                _responce = ReadSerie(BANXICO_SERIE_TIPOCAMBIOEURO, fecha);

            if (_responce != null)
                _result = _responce.seriesResponse.series[0].Data[0].Data;
            return _result;
        }

    }
}


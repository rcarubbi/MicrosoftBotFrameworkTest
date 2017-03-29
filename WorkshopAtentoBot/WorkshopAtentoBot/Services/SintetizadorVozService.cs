using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Services
{

    public class SintetizadorVozService
    {
        private static Authentication auth = new Authentication(CognitiveServicesAuthorizationProvider.API_KEY);

      

        public static async Task<byte[]> Dizer(string texto)
        {
           

            string accessToken = auth.GetAccessToken();

            string uri = "https://speech.platform.bing.com/synthesize";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            string ImpressionGUID = Guid.NewGuid().ToString();
          //  webRequest.Proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));

            webRequest.ContentType = "application/ssml+xml";
            webRequest.UserAgent = "SintetizadorVozService";
            string formatName = "riff-16khz-16bit-mono-pcm";
            webRequest.Headers.Add("X-MICROSOFT-OutputFormat", formatName);
            webRequest.Headers.Add("X-Search-AppId", "E8767073394F41BF9C9275DDF3C64B0F");
            webRequest.Headers.Add("X-Search-ClientID", "1ECFAE91408841A480F00935DC390960");

            webRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            webRequest.Method = "POST";
                                  
            string bodyTemplate = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"http://www.w3.org/2001/mstts\" xml:lang=\"pt-BR\"><voice xml:lang=\"pt-BR\" name=\"Microsoft Server Speech Text to Speech Voice (pt-BR, Daniel, Apollo)\">{0}</voice></speak>";
     
         
            string encodedXml = texto.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
 
            string body = string.Format(bodyTemplate, encodedXml);
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = await webRequest.GetRequestStreamAsync())
            {
                await outputStream.WriteAsync(bytes, 0, bytes.Length);
            }

            WebResponse webResponse = await webRequest.GetResponseAsync();
            MemoryStream ms = new MemoryStream();
            using (Stream stream = webResponse.GetResponseStream())
            {
                stream.CopyTo(ms);
            }
            return ms.ToArray();
        }
    }
}
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
            byte[] output = null;

            string accessToken = auth.GetAccessToken();

            string uri = "https://speech.platform.bing.com/synthesize";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            string ImpressionGUID = Guid.NewGuid().ToString();

            webRequest.ContentType = "application/ssml+xml";
            webRequest.UserAgent = "SintetizadorVozService";
            string formatName = "riff-16khz-16bit-mono-pcm";
            webRequest.Headers.Add("X-MICROSOFT-OutputFormat", formatName);
            webRequest.Headers.Add("X-Search-AppId", "07D3234E49CE426DAA29772419F436CA");
            webRequest.Headers.Add("X-Search-ClientID", "1ECFAE91408841A480F00935DC390960");

            webRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            webRequest.Method = "POST";

            string bodyTemplate = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xmlns:mstts=\"http://www.w3.org/2001/mstts\" xmlns:emo=\"http://www.w3.org/2009/10/emotionml\" xml:lang=\"{0}\">{1}<emo:emotion><emo:category name=\"CALM\" value=\"1.0\"/><prosody rate=\"{2:F1}\">{3}</prosody></emo:emotion></voice></speak>";
            string voiceTag = "<voice name=\"Microsoft Server Speech Text to Speech Voice (pt-BR, Daniel, Apollo)\">";
            string deviceLanguage = "pt-BR";
            string encodedXml = texto.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");


            var prosodyRate = 0.1f;


            string body = string.Format(bodyTemplate, deviceLanguage, voiceTag, prosodyRate, encodedXml);
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = await webRequest.GetRequestStreamAsync())
            {
                await outputStream.WriteAsync(bytes, 0, bytes.Length);
            }

            WebResponse webResponse = await webRequest.GetResponseAsync();
            using (Stream stream = webResponse.GetResponseStream())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = await stream.ReadAsync(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (stream.CanRead && count > 0);
                    output = ms.ToArray();
                }
            }
            return output;
        }
    }
}
using NAudio.Lame;
using NAudio.Wave;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Services
{
    public class WavToMp3Converter
    {
        public async Task<byte[]> ConvertWavToMp3(byte[] wavFile)
        {

            using (var retMs = new MemoryStream())
            using (var ms = new MemoryStream(wavFile))
            using (var rdr = new WaveFileReader(ms))
            using (var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, 128))
            {
                await rdr.CopyToAsync(wtr);
                return retMs.ToArray();
            }


        }

        public async Task<string> Convert(byte[] wavdata)
        {


            var client = MakeJobClient(string.Empty);
            var postRequest = MakeRequest(Method.POST);
            postRequest.AddParameter("application/json", "{ \"conversion\":[{\"category\":\"audio\",\"target\":\"ogg\"}]}", ParameterType.RequestBody);
            IRestResponse<ConvertionJob> postResponse = await client.ExecuteTaskAsync<ConvertionJob>(postRequest);

            IRestResponse<StatusJob> statusResponse = null;
            var getRequest = MakeRequest(Method.GET);
            client = MakeJobClient(postResponse.Data.Id);
            do
            {
                statusResponse = await client.ExecuteTaskAsync<StatusJob>(getRequest);
            }
            while (statusResponse.Data.Status.Code != "incomplete");

            client = MakeUploadClient(statusResponse.Data.Server, postResponse.Data.Id);
            var uploadRequest = MakeRequest(Method.POST);
            uploadRequest.AddFileBytes("file", wavdata, Path.ChangeExtension(Path.GetTempFileName(), "wav"));
            uploadRequest.AlwaysMultipartFormData = true;
            var request = client.Post(uploadRequest);


            getRequest = MakeRequest(Method.GET);
            client = MakeJobClient(postResponse.Data.Id);
            do
            {
                await Task.Delay(500);
                statusResponse = await client.ExecuteTaskAsync<StatusJob>(getRequest);
            }
            while (statusResponse.Data.Status.Code != "completed");


            client = MakeJobClient(postResponse.Data.Id + "/output");
            getRequest = MakeRequest(Method.GET);

            var outputResponse = await client.ExecuteTaskAsync<List<OutputJob>>(getRequest);

            

            return outputResponse.Data[0].Uri;
        }

        private RestRequest MakeRequest(Method method)
        {
            var request = new RestRequest(method);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-oc-api-key", "8a8902cdd8b4a998358d885d3e3ea162");
            return request;
        }

        private RestClient MakeUploadClient(string server, string id)
        {
            //  var proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));
            var client = new RestClient(server + "/upload-file/" + id);

            //  client.Proxy = proxy;
            return client;
        }

        private RestClient MakeJobClient(string sufixo)
        {
            if (sufixo.Length > 0)
                sufixo = "/" + sufixo;

            //      var proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));
            var client = new RestClient("http://api2.online-convert.com/jobs" + sufixo);
            //     client.Proxy = proxy;
            return client;
        }

    }
}
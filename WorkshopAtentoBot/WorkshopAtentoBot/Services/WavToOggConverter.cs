using RestSharp;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WorkshopAtentoBot.Services
{
    public class WavToOggConverter
    {



        public async Task<MemoryStream> Convert(byte[] wavdata)
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

            var webClient = new WebClient();
         //   webClient.Proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));
            var data = await webClient.DownloadDataTaskAsync(outputResponse.Data[0].Uri);

            return new MemoryStream(data);
        }

        private RestRequest MakeRequest(Method method)
        {
            var request = new RestRequest(method);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-oc-api-key", "70c45eaa48166d299106d6e163340a13");
            return request;
        }

        private RestClient MakeUploadClient(string server, string id)
        {
          //  var proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));
            var client = new RestClient(server+ "/upload-file/" + id);

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
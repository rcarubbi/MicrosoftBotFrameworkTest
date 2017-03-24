using RestSharp;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WorkshopAtentoBot.Services
{
    public class OgaToWavConverter
    {

        public OgaToWavConverter()
        {

        }



        public async Task<Stream> Convert(string url)
        {


            var client = MakeClient(string.Empty);
            var postRequest = MakeRequest(Method.POST);
            postRequest.AddParameter("application/json", "{\"input\":[{\"type\":\"remote\",\"source\":\"" + url + "\"}],\"conversion\":[{\"category\":\"audio\",\"target\":\"wav\"}]}", ParameterType.RequestBody);
            IRestResponse<ConvertionJob> postResponse = await client.ExecuteTaskAsync<ConvertionJob>(postRequest);
            if (postResponse.Data == null || string.IsNullOrEmpty(postResponse.Data.Id)) throw new ApplicationException("Serviço de conversão de audio indisponível");

            IRestResponse<StatusJob> statusResponse = null;
            var getRequest = MakeRequest(Method.GET);

            
            client = MakeClient(postResponse.Data.Id);
            do
            {
                statusResponse = await client.ExecuteTaskAsync<StatusJob>(getRequest);
            }
            while (statusResponse.Data.Status.Code != "completed");

            client = MakeClient(postResponse.Data.Id + "/output");
            getRequest = MakeRequest(Method.GET);

            var outputResponse = await client.ExecuteTaskAsync<List<OutputJob>>(getRequest);

            var webClient = new WebClient();
            // webClient.Proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));
            var data = await webClient.DownloadDataTaskAsync(outputResponse.Data[0].Uri);

            return new MemoryStream(data);
        }

        private RestRequest MakeRequest(Method method)
        {
            var request = new RestRequest(method);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-oc-api-key", "8a8902cdd8b4a998358d885d3e3ea162");


            return request;
        }

        private RestClient MakeClient(string sufixo)
        {
            if (sufixo.Length > 0)
                sufixo = "/" + sufixo;

            //    var proxy = new WebProxy("http://proxycorpplus.atento.br:6666", true, new string[] { }, new NetworkCredential(@"atentobr\ab1177493", "Raphakf@02"));
            var client = new RestClient("http://api2.online-convert.com/jobs" + sufixo);
            //  client.Proxy = proxy;
            return client;
        }
    }
}
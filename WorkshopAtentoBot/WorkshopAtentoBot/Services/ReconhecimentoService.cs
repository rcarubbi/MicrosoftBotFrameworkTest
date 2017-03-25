using Microsoft.Bing.Speech;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Services
{
    public class ReconhecimentoService
    {


        public static async Task<string> ReconhecerFala(string contentUrl)
        {
            RecognitionStatus status = RecognitionStatus.None;
            string stringResult = string.Empty;
            WebClient wc = new WebClient();
            OgaToWavConverter converter = new OgaToWavConverter();
             

            var ogaData = await wc.DownloadDataTaskAsync(contentUrl);
            var wavData = converter.Convert(ogaData);

            var preferences = new Preferences("pt-BR",
                new Uri(@"wss://speech.platform.bing.com/api/service/recognition"),
                new CognitiveServicesAuthorizationProvider(CognitiveServicesAuthorizationProvider.API_KEY));
            using (var speechClient = new SpeechClient(preferences))
            {
                speechClient.SubscribeToPartialResult(
                   result =>
                   {
                       stringResult = result.DisplayText;
                       return Task.FromResult(true);
                   });

                speechClient.SubscribeToRecognitionResult(
                    result =>
                    {

                        status = result.RecognitionStatus;
                        return Task.FromResult(true);
                    });

                var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Unknown, NetworkType.Unknown, OsName.Windows, "10", "IBM", "ThinkCenter");
                var applicationMetadata = new ApplicationMetadata("WorkshopAtentoBot", "1.0");
                var requestMetada = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "ReconhecimentoFalaService");
                await speechClient.RecognizeAsync(new SpeechInput(new MemoryStream(wavData), requestMetada), CancellationToken.None).ConfigureAwait(false);
            }

            while (status == RecognitionStatus.None)
            {
                await Task.Delay(200);
            }

            if (status == RecognitionStatus.Success)
            {
                return stringResult;
            }
            else
            {
                return $"Ocorreu um erro no reconhecimento de fala. Status = {status}";
            }
        }



    }
}
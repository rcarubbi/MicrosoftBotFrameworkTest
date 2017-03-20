using Microsoft.Bing.Speech;
using System;
using System.Net;
using System.Net.Http;
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
            var oggData = await wc.DownloadDataTaskAsync(contentUrl);
            OggToWavConverter converter = new OggToWavConverter();
            var wavData = converter.Convert(oggData);
            var preferences = new Preferences("pt-BR",
                new Uri(@"wss://speech.platform.bing.com/api/service/recognition"),
                new CognitiveServicesAuthorizationProvider("4f5863d0331941888f4e27c119a08094"));
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

                var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Unknown, NetworkType.Unknown, OsName.Windows, string.Empty, string.Empty, string.Empty);
                var applicationMetadata = new ApplicationMetadata("WorkshopAtentoBot", "1.0");
                var requestMetada = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "ReconhecimentoFalaService");

                await speechClient.RecognizeAsync(new SpeechInput(wavData, requestMetada), CancellationToken.None).ConfigureAwait(false);
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


        public class CognitiveServicesAuthorizationProvider : IAuthorizationProvider
        {
            /// <summary>
            /// The fetch token URI
            /// </summary>
            private const string FetchTokenUri = "https://api.cognitive.microsoft.com/sts/v1.0";

            /// <summary>
            /// The subscription key
            /// </summary>
            private readonly string subscriptionKey;

            /// <summary>
            /// Initializes a new instance of the <see cref="CognitiveServicesAuthorizationProvider" /> class.
            /// </summary>
            /// <param name="subscriptionKey">The subscription identifier.</param>
            public CognitiveServicesAuthorizationProvider(string subscriptionKey)
            {
                if (subscriptionKey == null)
                {
                    throw new ArgumentNullException(nameof(subscriptionKey));
                }

                if (string.IsNullOrWhiteSpace(subscriptionKey))
                {
                    throw new ArgumentException(nameof(subscriptionKey));
                }

                this.subscriptionKey = subscriptionKey;
            }

            /// <summary>
            /// Gets the authorization token asynchronously.
            /// </summary>
            /// <returns>
            /// A task that represents the asynchronous read operation. The value of the string parameter contains the next the authorization token.
            /// </returns>
            /// <remarks>
            /// This method should always return a valid authorization token at the time it is called.
            /// </remarks>
            public Task<string> GetAuthorizationTokenAsync()
            {
                return FetchToken(FetchTokenUri, this.subscriptionKey);
            }

            /// <summary>
            /// Fetches the token.
            /// </summary>
            /// <param name="fetchUri">The fetch URI.</param>
            /// <param name="subscriptionKey">The subscription key.</param>
            /// <returns>An access token.</returns>
            private static async Task<string> FetchToken(string fetchUri, string subscriptionKey)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    var uriBuilder = new UriBuilder(fetchUri);
                    uriBuilder.Path += "/issueToken";

                    using (var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null).ConfigureAwait(false))
                    {
                        return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
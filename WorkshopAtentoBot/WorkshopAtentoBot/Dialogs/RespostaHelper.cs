using Microsoft.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WorkshopAtentoBot.Services;

namespace WorkshopAtentoBot.Dialogs
{


    public class RespostaHelper : IRespostaHelper
    {
     
        public async Task Responder(IDialogContext context, string text, string nome = "Resposta")
        {

            bool spokenAnswer = true;
            context.ConversationData.TryGetValue<bool>("spokenAnswer", out spokenAnswer);

            if (!spokenAnswer)
            {
                await context.PostAsync(text);
            }
            else
            {
                var wavData = await SintetizadorVozService.Dizer(text);
              
                var conv = new WavToMp3Converter();
                var mp3Data = conv.Convert(wavData);
                var answer  = context.MakeMessage();
                 
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));
              
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("tempfiles");
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                var blobName = Path.GetFileName(Path.ChangeExtension(Path.GetTempFileName(), "mp3"));
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
                blockBlob.UploadFromStream(new MemoryStream(mp3Data));

                answer.Text = $"https://atentoworkshopbot.blob.core.windows.net/tempfiles/{blobName}";
              //  answer.Attachments = new List<Attachment>();
              //  answer.Attachments.Add(new Attachment { ContentUrl = $"https://atentoworkshopbot.blob.core.windows.net/tempfiles/{blobName}", ContentType = "audio/mp3", Name = nome});
                                                                  
                await context.PostAsync(answer, CancellationToken.None);
               
            }
        }
    }
}
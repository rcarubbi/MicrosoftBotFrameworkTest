using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
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
            //context.UserData.TryGetValue<bool>("spokenAnswer", out spokenAnswer);

            if (!spokenAnswer)
            {
                await context.PostAsync(text);
            }
            else
            {
                var wavData = await SintetizadorVozService.Dizer(text);
              
                var conv = new WavToMp3Converter();
                var mp3Data = await conv.ConvertWavToMp3(wavData);
                var answer  = context.MakeMessage();

                 
                answer.Attachments = new List<Attachment>();
                answer.Attachments.Add(new Attachment { Content = mp3Data, ContentType = "audio/mp3", Name = nome});
                await context.PostAsync(answer, CancellationToken.None);
               
            }
        }
    }
}
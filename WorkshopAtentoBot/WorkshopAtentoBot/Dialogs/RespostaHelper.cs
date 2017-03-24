using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkshopAtentoBot.Services;

namespace WorkshopAtentoBot.Dialogs
{
 
    
    public class RespostaHelper : IRespostaHelper
    {
     
        public async Task Responder(IDialogContext context, string text, string nome = "Resposta")
        {

            bool spokenAnswer = true;
            context.UserData.TryGetValue<bool>("spokenAnswer", out spokenAnswer);



            if (!spokenAnswer)
            {
                await context.PostAsync(text);
            }
            else
            {
               
                var wavData = await SintetizadorVozService.Dizer(text);
              
                // var conv = new WavToOggConverter();
                // var oggData = await conv.Convert(wavData);
                 var answer  = context.MakeMessage();
                
               
                answer.Attachments = new List<Attachment>();
                answer.Attachments.Add(new Attachment { Content = wavData, ContentType = "audio/wav", Name = nome});
                await context.PostAsync(answer);
               
            }
        }
    }
}
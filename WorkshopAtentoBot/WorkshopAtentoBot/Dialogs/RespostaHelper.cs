using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;
using WorkshopAtentoBot.Services;

namespace WorkshopAtentoBot.Dialogs
{
 
   
    public class RespostaHelper
    {
        private OutputType _type;

        public OutputType Type
        {
            get
            {
                return _type;
            }
        }

        public RespostaHelper(OutputType type)
        {
            _type = type;
        }

        public async Task Responder(IDialogContext context, string text)
        {

            if (_type == OutputType.Text)
            {
                await context.PostAsync(text);
            }
            else
            {
                var answer = Activity.CreateMessageActivity();

                answer.Attachments.Add(new Attachment { Content = await SintetizadorVozService.Dizer(text), ContentType = "audio/wav" });
                await context.PostAsync(answer);
            }
        }
    }
}
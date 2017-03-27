using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    [Serializable]
    public class SaudacaoDialog : IDialog
    {


        private static readonly IRespostaHelper _respostaHelper = ServiceResolver.Get<IRespostaHelper>();



        public async Task StartAsync(IDialogContext context)
        {
            await Respond(context);
            context.Wait(MessageReceiveAsync);
        }


        

        private async Task Respond(IDialogContext context)
        {
            var userName = String.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                  await _respostaHelper.Responder(context, "Olá, eu sou BDA, o Bot Digital da Atento! Qual o seu nome?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await _respostaHelper.Responder(context, $"Tudo bem {userName}? Como posso te ajudar hoje?");
            }
        }

        public virtual async Task MessageReceiveAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var userName = string.Empty;
            var getName = false;
            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
                await Respond(context);

            }

           
            context.Done(message);

        }
    }


}
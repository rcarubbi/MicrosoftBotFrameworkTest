﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    [Serializable]
    public class SaudacaoDialog : IDialog
    {
        [field: NonSerialized()]
        protected Activity _message;

        private static readonly IRespostaHelper _respostaHelper = ServiceResolver.Get<IRespostaHelper>();

        public SaudacaoDialog()
        {

        }

        public SaudacaoDialog(Activity message)
        {
            _message = message;
        }

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
                await _respostaHelper.Responder(_message, context, "Olá, eu sou Luis! Qual o seu nome?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await _respostaHelper.Responder(_message, context, $"Olá {userName}. Como posso te ajudar hoje?");
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
            }

            await Respond(context);
            context.Done(message);
        }
    }


}
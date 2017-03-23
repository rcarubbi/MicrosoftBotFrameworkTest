using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    [Serializable]
    public class MudarNomeDialog : IDialog
    {
        [field: NonSerialized()]
        protected Activity _message;

        private static readonly IRespostaHelper _respostaHelper = ServiceResolver.Get<IRespostaHelper>();

        public MudarNomeDialog()
        {

        }

        public MudarNomeDialog(Activity message)
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
            var novoNome = String.Empty;
            context.UserData.TryGetValue<string>("NovoNome", out novoNome);
            if (string.IsNullOrEmpty(novoNome))
            {
                await _respostaHelper.Responder(_message, context, "Como você quer que eu te chame?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await _respostaHelper.Responder(_message, context, $"A partir de agora te chamarei de {novoNome}.");
                context.UserData.SetValue<string>("NovoNome", string.Empty);
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
                context.UserData.SetValue<string>("NovoNome", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }

            await Respond(context);
            context.Done(message);
        }
    }
}
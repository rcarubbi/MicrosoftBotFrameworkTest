using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;
using WorkshopAtentoBot.Models;

namespace WorkshopAtentoBot.Dialogs
{
    [LuisModel("ce181128-0512-448e-a9ed-052be93058a9", "4c04858761a6404bbff14d30c2c01d63")]
    [Serializable]
    public class LUISDialog : LuisDialog<Pergunta>
    {
        private Pergunta _pergutna;
        public LUISDialog(Pergunta pergunta)
        {
            _pergutna = pergunta;
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe, eu não entendi o que você disse. Poderia Repetir?");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Saudacao")]
        public async Task Saudacao(IDialogContext context, LuisResult result)
        {
            context.Call(new SaudacaoDialog(), Callback);
        }

        [LuisIntent("PresidenteAtento")]
        public async Task PresidenteAtento(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("O presidente da atento no Brasil é Mário Câmara.");
            context.Wait(MessageReceived);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}
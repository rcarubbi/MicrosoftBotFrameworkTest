using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    public interface IRespostaHelper
    {
        
        Task Responder(IDialogContext context, string text, string nome = "Resposta");
    }
}

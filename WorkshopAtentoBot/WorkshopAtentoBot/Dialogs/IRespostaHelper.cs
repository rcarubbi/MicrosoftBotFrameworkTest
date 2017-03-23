using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    public interface IRespostaHelper
    {
        
        Task Responder(Activity message, IDialogContext context, string text, string nome = "Resposta");
    }
}

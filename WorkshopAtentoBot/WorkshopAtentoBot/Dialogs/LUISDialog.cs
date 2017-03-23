using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkshopAtentoBot.Models;

namespace WorkshopAtentoBot.Dialogs
{
    [LuisModel("ce181128-0512-448e-a9ed-052be93058a9", "4c04858761a6404bbff14d30c2c01d63")]
    [Serializable]
    public class LUISDialog : LuisDialog<object>
    {

        [field: NonSerialized()]
        protected Activity _message;


        private static readonly IRespostaHelper _respostaHelper = ServiceResolver.Get<IRespostaHelper>();



      
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(_message, context, "Desculpe, eu não entendi o que você disse. Poderia Repetir?", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Saudacao")]
        public async Task Saudacao(IDialogContext context, LuisResult result)
        {
            context.Call(new SaudacaoDialog(_message), Callback);
        }

        [LuisIntent("MudarNome")]
        public async Task MudarNome(IDialogContext context, LuisResult result)
        {
            context.Call(new MudarNomeDialog(_message), Callback);
        }

        [LuisIntent("Atento")]
        public async Task Atento(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "Líder global e maior empresa de CRM BiPiOu na América Latina. Fundada em 1999 como fornecedora para o Grupo Telefônica adquirida pela Bem quépital em 2012.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("AtentoBrasil")]
        public async Task AtentoBrasil(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "A Atento Brasil é a terceira maior empregadora no Brasil, mais de setenta e cinco mil funcionários, mais de quarenta e duas mil posições, 34 centrais próprias e 36  remotas.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("BNDES")]
        public async Task BNDES(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "A Atento Brasil possui financiamento para o desenvolvimento com recursos do B.N.D.E.S.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Bolsa")]
        public async Task Bolsa(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "A Atento possui ações na bolsa de Nova Yorque e o seu código é A.T.T.O.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("CEO")]
        public async Task CEO(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "Alerrandro Reynal Ample é o Ci-i-Ou da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("CFO")]
        public async Task CFO(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "Mauricio Montilha é o Chief Financial Officer (CFO) da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Cafe")]
        public async Task Cafe(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "Servimos mais de 18 milhões de cafés para os funcionários do Brasil.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Certificacoes")]
        public async Task Certificacoes(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "A Atento possui as certificações COPC e ISO 9001 2008.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Clientes")]
        public async Task Clientes(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "São mais de 400 clientes em todo o mundo.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Comercial")]
        public async Task Comercial(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "Daniel Figueirido é o diretor Comercial Global da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Consciencia")]
        public async Task Consciencia(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(_message, context, "175 toneladas de equipamentos reciclados em 2016.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("DiretorRegional")]
        public async Task DiretorRegional(IDialogContext context, LuisResult result)
        {
           var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

           await _respostaHelper.Responder(_message, context, $"Voce quer saber o diretor regional da região: {regiao}", result.Query);
           context.Wait(MessageReceived);
        }

        [LuisIntent("Empregados")]
        public async Task Empregados(IDialogContext context, LuisResult result)
        {
            var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(_message, context, $"No mundo somos mais de 150.000, sendo mais de 75 mil no Brasil.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Foco")]
        public async Task Foco(IDialogContext context, LuisResult result)
        {
            var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(_message, context, $"Nosso foco: Clientes; Inovação, Excelência e Qualidade; Experiência dos Consumidores.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Frost_Sullivan")]
        public async Task Frost_Sullivan(IDialogContext context, LuisResult result)
        {
            var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(_message, context, $"Frost & Sullivan reconheceu a Atento como o prêmio de Líder de Mercado no México.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("GPTW")]
        public async Task GPTW(IDialogContext context, LuisResult result)
        {
            var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(_message, context, $"O Great Place to Work nos reconheceu mais uma vez em 2016 e nos colocou entre as 25 melhores empresas multinacionais para trabalhar.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Gartner")]
        public async Task Gartner(IDialogContext context, LuisResult result)
        {
            var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(_message, context, $"A Atento foi nomeada a líder no quadrante mágico do Gartner em Customer Management Contact Center BPO.", result.Query);
            context.Wait(MessageReceived);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        protected override async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            _message = (Activity)await item;
            await base.MessageReceived(context, item);
        }
    }


}
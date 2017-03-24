using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    [LuisModel("ce181128-0512-448e-a9ed-052be93058a9", "4c04858761a6404bbff14d30c2c01d63")]
    [Serializable]
    public class LUISDialog : LuisDialog<object>
    {

     
        private static readonly IRespostaHelper _respostaHelper = ServiceResolver.Get<IRespostaHelper>();



      
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, "Desculpe, eu não entendi o que você disse. Poderia Repetir?", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Saudacao")]
        public async Task Saudacao(IDialogContext context, LuisResult result)
        {
            context.Call(new SaudacaoDialog(), Callback);
        }

        [LuisIntent("MudarNome")]
        public async Task MudarNome(IDialogContext context, LuisResult result)
        {
            context.Call(new MudarNomeDialog(), Callback);
        }

        [LuisIntent("Atento")]
        public async Task Atento(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "Líder global e maior empresa de CRM BPO na América Latina. Fundada em 1999 como fornecedora para o Grupo Telefônica adquirida pela Bain Capital em 2012.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("AtentoBrasil")]
        public async Task AtentoBrasil(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "A Atento Brasil é a terceira maior empregadora no Brasil, mais de setenta e cinco mil funcionários, mais de quarenta e duas mil posições, 34 centrais próprias e 36 remotas.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("BNDES")]
        public async Task BNDES(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "A Atento Brasil possui financiamento para o desenvolvimento com recursos do BNDES.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Bolsa")]
        public async Task Bolsa(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "A Atento possui ações na bolsa de Nova Yorque e o seu código é A T T O.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("CEO")]
        public async Task CEO(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "Alerrandro Reynal Ample é o CEO da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("CFO")]
        public async Task CFO(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "Mauricio Montilha é o Chief Financial Officer (CFO) da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Cafe")]
        public async Task Cafe(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "Servimos mais de 18 milhões de cafés para os funcionários do Brasil.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Certificacoes")]
        public async Task Certificacoes(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "A Atento possui as certificações COPC e ISO 9001 2008.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Clientes")]
        public async Task Clientes(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "São mais de 400 clientes em todo o mundo.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Comercial")]
        public async Task Comercial(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "Sílvio Solteiro é o diretor Comercial da Atento no Brasil.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Consciencia")]
        public async Task Consciencia(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, "175 toneladas de equipamentos reciclados em 2016.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("DiretorRegional")]
        public async Task DiretorRegional(IDialogContext context, LuisResult result)
        {
           var regiao = result.Entities.FirstOrDefault()?.Entity ?? "";

            if (regiao.ToLower() == "brasil")
            {
                await _respostaHelper.Responder(context, $"Mário Câmara é o diretor regional do Brasil. E você, conhece o Mário?", result.Query);
            }
            else if (regiao.ToLower() == "américa")
            {
                await _respostaHelper.Responder(context, $"Miguel Matey Marañon é o diretor Regional para a América do Norte.", result.Query);
            }
           context.Wait(MessageReceived);
        }

        [LuisIntent("Empregados")]
        public async Task Empregados(IDialogContext context, LuisResult result)
        {
          

            await _respostaHelper.Responder(context, $"No mundo somos mais de 150.000, sendo mais de 75 mil no Brasil.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Foco")]
        public async Task Foco(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"Nosso foco: Clientes; Inovação, Excelência e Qualidade; Experiência dos Consumidores.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Frost_Sullivan")]
        public async Task Frost_Sullivan(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"Frost & Sullivan reconheceu a Atento como o prêmio de Líder de Mercado no México.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("GPTW")]
        public async Task GPTW(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"O Great Place to Work nos reconheceu mais uma vez em 2016 e nos colocou entre as 25 melhores empresas multinacionais para trabalhar.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Gartner")]
        public async Task Gartner(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"A Atento foi nomeada a líder no quadrante mágico do Gartner em Customer Management Contact Center BPO.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Interfile")]
        public async Task Interfile(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"Em 2017 a Atento anunciou a aquisição da participação majoritária da Interfile para formar o maior provedor de soluções de BPO.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Juridico")]
        public async Task Juridico(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"Reyes Cerezo é a diretora de Conformidade Legal e Normativa da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Metodologia")]
        public async Task Metodologia(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"A Atento utiliza a metodologia Six Sigma.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Missao")]
        public async Task Missao(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"Fazemos com que as empresas obtenham sucesso ao passo que garantimos a melhor experiência para os seus consumidores.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Mulher")]
        public async Task Mulher(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"São 2.373 mulheres na liderança da Atento Brasil.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Numeros")]
        public async Task Numeros(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"No mundo somos 96 sites e 90 mil workstations.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Operacoes")]
        public async Task Operacoes(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"Michael Flodin é o diretor de Operações da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Paises")]
        public async Task Paises(IDialogContext context, LuisResult result)
        {

            await _respostaHelper.Responder(context, $"A Atento está presente em 13 países.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Portifolio")]
        public async Task Portifolio(IDialogContext context, LuisResult result)
        {
            var portifolioNome = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(context, $"Voce quer saber sobre a opção de portifolio: {portifolioNome}", result.Query);
            context.Wait(MessageReceived);

        }

        [LuisIntent("Premio")]
        public async Task Premio(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, $"Em 2016 a Atento recebeu pela 8a. vez o prêmio Amauta em experiência do usuário.", result.Query);
            context.Wait(MessageReceived);
        }



        [LuisIntent("RH")]
        public async Task RH(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, $"Iñaki Cebollero é o diretor de Recursos Humanos da Atento.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Rbrasil")]
        public async Task Rbrasil(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, $"Em 2016 a Atento adquiriu a participação majoritária da R Brasil Soluções para expandir sua capacidade em serviços de cobrança.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Responsabilidade")]
        public async Task Responsabilidade(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, $"São mais de 1000 grávidas assistidas. Somos a primeira empresa BPO no mundo a obter a certificação de Responsabilidade Social SA8000.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Site")]
        public async Task Site(IDialogContext context, LuisResult result)
        {
            var regiaoNome = result.Entities.FirstOrDefault()?.Entity ?? "";

            await _respostaHelper.Responder(context, $"Você quer saber o endereço do site {regiaoNome}.", result.Query);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Solucoes")]
        public async Task Solucoes(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, $"A Atento possui soluções completas para vendas, atenção ao cliente, suporte técnico, cobrança e backoffice.", result.Query);
            context.Wait(MessageReceived);
        }


        [LuisIntent("Valores")]
        public async Task Valores(IDialogContext context, LuisResult result)
        {
            await _respostaHelper.Responder(context, $"Compromisso, Paixão, Integridade e Confiança são os valores da Atento.", result.Query);
            context.Wait(MessageReceived);
        }


        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

     
    }


}
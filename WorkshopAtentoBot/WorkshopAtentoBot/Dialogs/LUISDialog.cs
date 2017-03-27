using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Dialogs
{
    [LuisModel("ce181128-0512-448e-a9ed-052be93058a9", "9b8cc59376504cae8fe2cb67857a8d82")]
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
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                context.Call(new SaudacaoDialog(), Callback);
            }
            else
            {
                await _respostaHelper.Responder(context, $"Olá {userName}, como posso te ajudar hoje?", result.Query);
                context.Wait(MessageReceived);
            }
         
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
          
            EntityRecommendation sufixo, regiao;
            result.TryFindEntity("nomeregiao", out regiao);
            result.TryFindEntity("sufixonomeregiao", out sufixo);
            string regiaostr = regiao?.Entity?.ToLower()?.Trim() ?? string.Empty;
            string sufixostr = sufixo?.Entity?.ToLower()?.Trim() ?? string.Empty;

           

            if (regiaostr.Contains("brasil"))
            {
                await _respostaHelper.Responder(context, $"Mário Câmara é o diretor regional do Brasil. E você, conhece o Mário?", result.Query);
            }
            else if (regiaostr.Contains("américa") && sufixostr.Contains("norte"))
            {
                await _respostaHelper.Responder(context, $"Miguel Matey Marañon é o diretor Regional para a América do Norte.", result.Query);
            }
            else if (regiaostr.Contains("américa") && sufixostr.Contains("sul"))
            {
                await _respostaHelper.Responder(context, $"Juan Gamé é o diretor regional para a América do Sul.", result.Query);
            }
            else if (regiaostr.Contains("europa") || regiaostr.Contains("oriente") || regiaostr.Contains("áfrica"))
            {
                await _respostaHelper.Responder(context, $"José María Pérez Melber é o diretor regional para a EMEA.", result.Query);
            }
            else
            {
                await _respostaHelper.Responder(context, "Desculpe, eu não entendi o que você disse. Pode Repetir?", result.Query);
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
            EntityRecommendation nomeportifolio;
            result.TryFindEntity("nomeportifolio", out nomeportifolio);
            string portifoliostr = nomeportifolio?.Entity?.ToLower()?.Trim() ?? string.Empty;

            if (portifoliostr.Contains("vendas"))
            {
                await _respostaHelper.Responder(context, $"Solução completa da Atento para atender a todos os processos de vendas das empresas – desde a identificação de consumidores potenciais até o pós-vendas.", result.Query);
            }
            else if (portifoliostr.Contains("b2b"))
            {
                await _respostaHelper.Responder(context, $"Apoia as empresas na definição das estratégias de venda, canais de relacionamento e gestão de carteiras de clientes, gerando melhores resultados e performance.", result.Query);
            }
            else if (portifoliostr.Contains("customer"))
            {
                await _respostaHelper.Responder(context, $"Solução desenvolvida para gerenciar a jornada completa do atendimento ao consumidor, fornecendo informações e respondendo às sugestões, solicitações e reclamações relacionadas a produtos e serviços.", result.Query);
            }
            else if (portifoliostr.Contains("suporte"))
            {
                await _respostaHelper.Responder(context, $"Solução para gestão centralizada, diagnóstico e resolução remota de incidentes técnicos.", result.Query);
            }
            else if (portifoliostr.Contains("cobrança"))
            {
                await _respostaHelper.Responder(context, $"Nossa solução de cobrança recupera o crédito em faixas de atraso avançadas, com estrutura operacional e de inteligência centralizada, por meio de estratégia multicanal, obtendo maior recuperação e menor custo.", result.Query);
            }
            else if (portifoliostr.Contains("office"))
            {
                await _respostaHelper.Responder(context, $"A solução de Back Office permite que as empresas priorizem as ações relacionadas ao seu core business, criando uma estrutura de processos para gerenciar com inteligência e eficiência todas as atividades administrativas e de suporte, focando na rentabilidade dos negócios.", result.Query);
            }
            else if (portifoliostr.Contains("reclama"))
            {
                await _respostaHelper.Responder(context, $"Temos solução para prevenção e gestão de reclamações e contenciosos em quaisquer instâncias – SAC, Ouvidoria, Órgãos Reguladores, Órgãos de Defesa do Consumidor e Juizado Especial Civil – para empresas de todos os segmentos.", result.Query);
            }
            else if (portifoliostr.Contains("crédito"))
            {
                await _respostaHelper.Responder(context, $"Nossa solução de Crédito contempla a análise e concessão de crédito, gerencia a formalização de contratos, oferece vendas e suporte para atendimento aos clientes, bem como a realização de serviços de cobrança.", result.Query);
            }
            else if (portifoliostr.Contains("pagamento"))
            {
                await _respostaHelper.Responder(context, $"Atuamos em toda a cadeia de valor de Emissores e Credenciadoras/Adquirentes de cartões, utilizando ferramentas de inteligência e múltiplos canais, para aumentar a rentabilidade da carteira e proporcionar a melhor experiência do consumidor.", result.Query);
            }
            else if (portifoliostr.Contains("trade"))
            {
                await _respostaHelper.Responder(context, $"A solução Trade Marketing executa a estratégia das empresas no PDV (Ponto de Venda), de acordo com as características de cada canal e o perfil do comprador, pois se adequa às necessidades das áreas de marketing e vendas.", result.Query);
            }
            else if (portifoliostr.Contains("multi"))
            {
                await _respostaHelper.Responder(context, $"Para suportar todas as soluções, a estratégia de multicanalidade da Atento, oferece a integração de canais e tecnologias, proporcionando a melhor experiência aos clientes.", result.Query);
            }
            else if (portifoliostr.Contains("channel"))
            {
                await _respostaHelper.Responder(context, $"O atendimento unificado permite a identificação do consumidor e de sua solicitação, independente da origem ou mesmo da transição entre os canais, mantendo todo o histórico de interação.", result.Query);
            }
            else
            {
                await _respostaHelper.Responder(context, "Desculpe, eu não entendi o que você disse. Pode Repetir?", result.Query);
            }
            
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
            EntityRecommendation nomesite, nomesite2, nomesite3;
            result.TryFindEntity("nomesite", out nomesite);
            result.TryFindEntity("nomesite2", out nomesite2);
            result.TryFindEntity("nomesite3", out nomesite3);
            string nomesitestr = nomesite?.Entity?.ToLower()?.Trim() ?? string.Empty;
            string nomesite2str = nomesite2?.Entity?.ToLower()?.Trim() ?? string.Empty;
            string nomesite3str = nomesite3?.Entity?.ToLower()?.Trim() ?? string.Empty;
            
            if (nomesitestr.Contains("barra"))
            {
                await _respostaHelper.Responder(context, $"O site Barra Funda fica na Avenida Antártica, 214, Barra Funda, São Paulo, CEP 01141-060.", result.Query);
            }
            else if (nomesitestr.Contains("belem"))
            {
                await _respostaHelper.Responder(context, $"O site Belém fica na Rua Padre Adelino, 550, Belém, São Paulo, CEP 03303-000.", result.Query);
            }
            else if (nomesitestr.Contains("belo") || nomesitestr.Contains("bh"))
            {
                await _respostaHelper.Responder(context, $"O site Belo Horizonte - Prado fica na Rua Jaceguai, 220, Prado, Belo Horizonte, CEP 30410-510.", result.Query);
            }
            else if (nomesitestr.Contains("casa"))
            {
                await _respostaHelper.Responder(context, $"O site Casa fica na Avenida Alfredo Egídio de Souza Aranha, 100, Chácara Santo Antonio, São Paulo, CEP 04726-170.", result.Query);
            }
            else if (nomesitestr.Contains("cidade"))
            {
                await _respostaHelper.Responder(context, $"O site Cidade Nova fica na Rua Pinto de Azevedo, 105, Cidade Nova, Rio de Janeiro, CEP 20211-445.", result.Query);
            }
            else if (nomesitestr.Contains("feira"))
            {
                await _respostaHelper.Responder(context, $"O site Feira de Santana fica na Pça Carlos Bahia, s/n, Centro, Feira de Santana, CEP 44002-772.", result.Query);
            }
            else if (nomesitestr.Contains("goiania"))
            {
                await _respostaHelper.Responder(context, $"O site Goiania fica na Rua 136C, 150 - Qd. F44, Setor Sul, Goiânia, CEP 74093-280.", result.Query);
            }
            else if (nomesitestr.Contains("liberdade"))
            {
                await _respostaHelper.Responder(context, $"O site Liberdade fica na Rua Barão de Iguape, 110, Liberdade, São Paulo, CEP 01507-000.", result.Query);
            }
            else if (nomesitestr.Contains("madureira"))
            {
                await _respostaHelper.Responder(context, $"O site Madureira fica na Rua João Vicente 187 , adureira, Rio de Janeiro, CEP 21340-020.", result.Query);
            }
            else if (nomesitestr.Contains("major"))
            {
                await _respostaHelper.Responder(context, $"O site Major Fonseca fica na Rua Major Fonseca, 21, São Cristóvão , Rio de Janeiro, CEP 20920-040.", result.Query);
            }
            else if (nomesitestr.Contains("mauá"))
            {
                await _respostaHelper.Responder(context, $"O site Major Fonseca fica na Rua Major Fonseca, 21, São Cristóvão , Rio de Janeiro, CEP 20920-040.O site Mauá - CT fica na Rua Conselheiro Saraiva, 28, Centro, Rio de Janeiro, CEP 20091-030.", result.Query);
            }
            else if (nomesitestr.Contains("nova"))
            {
                await _respostaHelper.Responder(context, $"O site Nova São Paulo fica na Rua Professor Manoelito de Ornellas, 303 - Térreo, 1º,2º,3º, 4º,5º e 8º andares, Chácara Santo Antonio, São Paulo, CEP 04719-040", result.Query);
            }
            else if (nomesitestr.Contains("oliveira"))
            {
                await _respostaHelper.Responder(context, $"O site Oliveira Coutinho fica na Rua José de Oliveira Coutinho, 73, Barra Funda, São Paulo, CEP 01144-020.", result.Query);
            }
            else if (nomesitestr.Contains("penha"))
            {
                await _respostaHelper.Responder(context, $"O site Penha fica na Rua Lobo Júnior, 1795, Penha, Rio de Janeiro, 21020-123.", result.Query);
            }
            else if (nomesitestr.Contains("porto"))
            {
                await _respostaHelper.Responder(context, $"O site Porto Alegre fica na Avenida Júlio de Castilhos, 505, Centro, Porto Alegre, CEP 90030-131.", result.Query);
            }
            else if (nomesitestr.Contains("república"))
            {
                await _respostaHelper.Responder(context, $"O site República fica na Entrada 1 - Praça da República, 295 do 1º ao 10º andar, Entrada 2 - Rua Marques de Itu, 52, Centro, São Paulo, CEP 01223-001.", result.Query);
            }
            else if (nomesitestr.Contains("ribeirão"))
            {
                await _respostaHelper.Responder(context, $"O site Ribeirão Preto fica na Rua Antônio Fernandes Figueroa, 1400, Parque Industrial Lagoinha, Ribeirão Preto, CEP 14095-280.", result.Query);
            }
            else if (nomesitestr.Contains("rocha"))
            {
                await _respostaHelper.Responder(context, $"O site Rocha Verá fica na Av. das Nações Unidas, 14171 - Ebony Tower 4º Andar, Vila Gertrudes, São Paulo, CEP 04794-000.", result.Query);
            }
            else if (nomesitestr.Contains("cabula"))
            {
                await _respostaHelper.Responder(context, $"O site Cabula fica na Rua Silveira Martins, 1036, Cabula, Salvador, CEP 41150-000.", result.Query);
            }
            else if (nomesitestr.Contains("uruguai"))
            {
                await _respostaHelper.Responder(context, $"O site Uruguai fica na Rua Uruguai, 55 e 56, Uruguai, Salvador, CEP 40450-600.", result.Query);
            }
            else if (nomesitestr.Contains("santana"))
            {
                await _respostaHelper.Responder(context, $"O site Santana fica na Rua Voluntários da Pátria, 300, Santana, São Paulo, CEP 02011-000.", result.Query);
            }
            else if (nomesitestr.Contains("santo") && nomesite2str.Contains("andré"))
            {
                await _respostaHelper.Responder(context, $"O site Santo André fica na Av. Dom Pedro I, 530, Vila América, Santo André, CEP 09110-000.", result.Query);
            }
            else if (nomesitestr.Contains("santo") && nomesite2str.Contains("antônio"))
            {
                await _respostaHelper.Responder(context, $"O site Santo Antônio fica na Rua Domingos Marchetti, 77, Limão, São Paulo, CEP 02712-150.", result.Query);
            }
            else if (nomesitestr.Contains("santos"))
            {
                await _respostaHelper.Responder(context, $"O site Santos fica na Rua Visconde de São Leopoldo, 277, Centro, Santos, CEP 11010-201.", result.Query);
            }
            else if (nomesitestr.Contains("são") && nomesite2str.Contains("bento") && (nomesite3str.Contains("dois") || nomesite3str.Contains("2")))
            {
                await _respostaHelper.Responder(context, $"O site São Bento 2 fica na Largo São Bento, 64 - 1º andar e sobreloja, Centro, São Paulo, 01029-010.", result.Query);
            }
            else if (nomesitestr.Contains("são") && nomesite2str.Contains("bento"))
            {
                await _respostaHelper.Responder(context, $"O site São Bento fica na Rua Líbero Badaró, 633/641, Centro, São Paulo, 01009-000.", result.Query);
            }
            else if (nomesitestr.Contains("são") && nomesite2str.Contains("bernardo"))
            {
                await _respostaHelper.Responder(context, $"O site São Bernardo do Campo fica na Rua Wallace Simonsen, 13, Nova Petrópolis, São Bernardo do Campo, CEP 09771-210.", result.Query);
            }
            else if (nomesitestr.Contains("são") && nomesite2str.Contains("cristovão"))
            {
                await _respostaHelper.Responder(context, $"O site São Cristóvão fica na Rua São Luiz Gonzaga, 989, São Cristovão, Rio de Janeiro, CEP 20910-063.", result.Query);
            }
            else if (nomesitestr.Contains("são") && nomesite2str.Contains("josé"))
            {
                await _respostaHelper.Responder(context, $"O site São José dos Campos fica na Av. Andrômeda, 227, Jardim Satélite, São José dos Campos, CEP 12230-000.", result.Query);
            }
            else if (nomesitestr.Contains("teleporto"))
            {
                await _respostaHelper.Responder(context, $"O site Teleporto fica na Av. Presidente Vargas, 3131 - 6º, 7º e 18º andares, Cidade Nova, Rio de Janeiro, 20210-030.", result.Query);
            }
            else if (nomesitestr.Contains("zona"))
            {
                await _respostaHelper.Responder(context, $"O site Zona Sul fica na Av. das Nações Unidas, 19.847, Vila Almeida, São Paulo, 04730-090.", result.Query);
            }
            else if (nomesitestr.Contains("guarulhos"))
            {
                await _respostaHelper.Responder(context, $"O site Guarulhos fica na Av. Guarulhos, 3384, Centro, Guarulhos, SP, 07030-001.", result.Query);
            }
            else if (nomesitestr.Contains("campo"))
            {
                await _respostaHelper.Responder(context, $"O site Campo Grande fica na Rua Campo Grande, Lote 8, Campo Grande, RIo de Janeiro, RJ, CEP:23085-360.", result.Query);
            }
            else if (nomesitestr.Contains("são") && nomesite2str.Contains("caetano"))
            {
                await _respostaHelper.Responder(context, $"O site São Caetano do Sul fica na Rua Serafim Constantino, 100, Centro, Sâo Caetano de Sul, SP 09510-220.", result.Query);
            }
            else  
            {
                await _respostaHelper.Responder(context, $"Não consegui identificar qual site você deseja saber a localização. Pode repetir?", result.Query);
            }




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
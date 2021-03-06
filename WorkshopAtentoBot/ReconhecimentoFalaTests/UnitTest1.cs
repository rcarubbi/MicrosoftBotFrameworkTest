﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using WorkshopAtentoBot.Services;

namespace ReconhecimentoFalaTests   
{
    [TestClass]
    public class ReoonhecimentoFalaTests
    {
        [TestMethod]
        public async Task Teste()
        {
            var url = "https://bcattachmentsprod.blob.core.windows.net/636258816000000000/HBdjmtt2OrA/file_13.oga";
            var actual = await WorkshopAtentoBot.Services.ReconhecimentoService.ReconhecerFala(url);
            var expected = "Olá";

            Assert.AreEqual(expected, actual);
        }

      

 

        [TestMethod]
        public async Task TTS()
        {
            
            var texto = "Fazemos com que as empresas obtenham sucesso ao passo que garantimos a melhor experiência para os seus consumidores.";

            var bytes = await WorkshopAtentoBot.Services.SintetizadorVozService.Dizer(texto);
            Assert.IsTrue(bytes.Length > 0);


        }
    }
}
 
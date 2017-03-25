using RestSharp;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WorkshopAtentoBot.Services
{
    public class OgaToWavConverter
    {
        public byte[] Convert(byte[] ogaData)
        {
            var ogaFilename = Path.ChangeExtension(Path.GetTempFileName(), "oga");
            File.WriteAllBytes(ogaFilename, ogaData);
            var wavFilename = Path.ChangeExtension(Path.GetTempFileName(), "wav");
            var p = Process.Start("opusdec.exe", $"--rate 16000 {ogaFilename} {wavFilename}");
            p.WaitForExit();
            var output = File.ReadAllBytes(wavFilename);
            File.Delete(ogaFilename);
            File.Delete(wavFilename);

            return output;
        }



    }
}
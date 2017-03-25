using NAudio.Lame;
using NAudio.Wave;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Services
{
    public class WavToMp3Converter
    {
        public byte[] Convert(byte[] wavData)
        {
           
            var wavFileName = Path.ChangeExtension(Path.GetTempFileName(), "wav");
            File.WriteAllBytes(wavFileName, wavData);

            var mp3Filename = Path.ChangeExtension(Path.GetTempFileName(), "mp3");
            var p = Process.Start("lame.exe", $"{wavFileName} {mp3Filename}");
            p.WaitForExit();
            var output = File.ReadAllBytes(mp3Filename);
            File.Delete(mp3Filename);
            File.Delete(wavFileName);

            return output;
        }

    }
}
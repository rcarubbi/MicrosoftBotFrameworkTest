using NAudio.Wave;
using System.IO;

namespace WorkshopAtentoBot.Services
{
    internal class OggToWavConverter
    {
        internal Stream Convert(byte[] oggData)
        {
            WaveFormat waveFormat = new WaveFormat(8000, 16, 1);
            
            using (var vorbisStream = new NAudio.Vorbis.VorbisWaveReader(new MemoryStream(oggData)))
            using (WaveStream pcmStream = WaveFormatConversionStream.CreatePcmStream(vorbisStream))
            {
                var wavMs = new MemoryStream();
                WaveFileWriter.WriteWavFileToStream(wavMs, new NAudio.Wave.BufferedWaveProvider(waveFormat));
                wavMs.Position = 0;
                return wavMs;
            }
        }
    }
}
using NAudio.Lame;
using NAudio.Wave;
using System.IO;
using System.Threading.Tasks;

namespace WorkshopAtentoBot.Services
{
    public class WavToMp3Converter
    {
        public async Task<byte[]> ConvertWavToMp3(byte[] wavFile)
        {

            using (var retMs = new MemoryStream())
            using (var ms = new MemoryStream(wavFile))
            using (var rdr = new WaveFileReader(ms))
            using (var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, 128))
            {
                await rdr.CopyToAsync(wtr);
                return retMs.ToArray();
            }


        }
    
    }
}
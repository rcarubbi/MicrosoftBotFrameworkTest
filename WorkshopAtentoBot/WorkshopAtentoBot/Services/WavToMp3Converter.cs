using System.Diagnostics;
using System.IO;
using System.Web;

namespace WorkshopAtentoBot.Services
{
    public class WavToMp3Converter
    {
        public byte[] Convert(byte[] wavData)
        {
           
            var wavFileName = Path.Combine(HttpContext.Current.Server.MapPath("~/bin"), Path.ChangeExtension(Path.GetTempFileName(), "wav"));
            File.WriteAllBytes(wavFileName, wavData);

            var mp3Filename = Path.Combine(HttpContext.Current.Server.MapPath("~/bin"), Path.ChangeExtension(Path.GetTempFileName(), "mp3"));
            var p = Process.Start(Path.Combine(HttpContext.Current.Server.MapPath("~/services"), "lame.exe"), $"{wavFileName} {mp3Filename}");
            p.WaitForExit();
            var output = File.ReadAllBytes(mp3Filename);
            File.Delete(mp3Filename);
            File.Delete(wavFileName);

            return output;
        }

    }
}
using System.Diagnostics;
using System.IO;
using System.Web;

namespace WorkshopAtentoBot.Services
{
    public class OgaToWavConverter
    {
        public byte[] Convert(byte[] ogaData)
        {
            var ogaFilename = Path.Combine(HttpContext.Current.Server.MapPath("~/bin"), Path.ChangeExtension(Path.GetTempFileName(), "oga"));
            File.WriteAllBytes(ogaFilename, ogaData);
            var wavFilename = Path.Combine(HttpContext.Current.Server.MapPath("~/bin"), Path.ChangeExtension(Path.GetTempFileName(), "wav"));
            var p = Process.Start(Path.Combine(HttpContext.Current.Server.MapPath("~/services"), "opusdec.exe"), $"--rate 16000 {ogaFilename} {wavFilename}");
            p.WaitForExit();
            var output = File.ReadAllBytes(wavFilename);
            File.Delete(ogaFilename);
            File.Delete(wavFilename);

            return output;
        }



    }
}
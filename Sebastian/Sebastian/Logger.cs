using System;
using System.Threading.Tasks;

namespace Sebastian
{
    internal class Logger
    {
        internal static void WriteLog(string fmt, params object[] args)
        {
            Console.WriteLine("[{0}]{1}", DateTime.Now.ToString(), string.Format(fmt, args));
        }
        internal static async Task WriteLogAsync(string fmt, params object[] args)
        {
            await Task.Run(() => WriteLog(fmt, args));
        }
    }
}

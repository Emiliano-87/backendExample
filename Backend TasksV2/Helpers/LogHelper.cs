using System.Runtime.CompilerServices;

namespace Backend.Helpers
{
    class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}

using Serilog.Core;
using VkApi.Wrapper.Services;

namespace ItmoSchedule.VK
{
    public class VkLibraryLogger : ILogger
    {
        private readonly Logger _vkFileLogger;

        public VkLibraryLogger(Logger vkFileLogger)
        {
            _vkFileLogger = vkFileLogger;
        }

        public void Log(object o)
        {
            if (o is string str)
                _vkFileLogger.Information(str);
            else
                _vkFileLogger.Information("{o}", o);
        }
    }
}
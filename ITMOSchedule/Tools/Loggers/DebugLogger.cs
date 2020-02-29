using System.Diagnostics;

namespace ItmoSchedule.Tools.Loggers
{
    public class DebugLogger : ILogger
    {
        public void Write(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
using System;
using System.IO;

namespace ItmoSchedule.Tools.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string message)
        {
            // ReSharper disable once LocalizableElement
            File.AppendAllText(_filePath, $"{message}{Environment.NewLine}");
        }
    }
}
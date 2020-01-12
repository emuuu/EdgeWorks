using Microsoft.Extensions.Logging;

namespace EdgeWorks.Shared.Configuration
{
    public class Logging
    {
        public string FilePath { get; set; }

        public bool WriteToFile { get; set; }

        public bool WriteToConsole { get; set; }

        public LogLevel MinimumLogLevel { get; set; }
    }
}

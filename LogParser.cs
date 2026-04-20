using System;

namespace LogAnalyzerTool
{
    public static class LogParser
    {
        public static string ExtractLogType(string line)
        {
            if (line.Contains("[ERROR]")) return "ERROR";
            if (line.Contains("[WARNING]")) return "WARNING";
            if (line.Contains("[INFO]")) return "INFO";
            if (line.Contains("[DEBUG]")) return "DEBUG";
            return "UNKNOWN";
        }
    }
}
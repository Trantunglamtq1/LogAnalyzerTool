using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LogAnalyzerTool
{
    public class SyncLogAnalyzer : ILogAnalyzer
    {
        public IDictionary<string, int> Analyze(string filePath)
        {
            var stats = new Dictionary<string, int>();

           
            foreach (var line in File.ReadLines(filePath))
            {
                string logType = LogParser.ExtractLogType(line);
                if (logType != "UNKNOWN")
                {
                    if (!stats.ContainsKey(logType)) stats[logType] = 1;
                    else stats[logType]++;
                }
            }
            return stats;
        }

        public Task<IDictionary<string, int>> AnalyzeAsync(string filePath)
            => Task.FromResult(Analyze(filePath)); 
    }
}